using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using moneyLionAssignment.Models;

namespace moneyLionAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : ControllerBase
    {
        private const string _dataFilePath = "./data.json";

        [HttpGet]
        public IActionResult Get(string featureName, string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(featureName) && !string.IsNullOrEmpty(email))
                {
                    if (System.IO.File.Exists(_dataFilePath))
                    {
                        var storedFeatures = GetStoredFeaturesInDataFile();

                        // Find a Feature with name and email equal to this method parametes.
                        var matchedFeature = storedFeatures.FirstOrDefault(feature => feature.FeatureName == featureName && feature.Email == email);
                        var result = ToFeatureAccessibilityObject(matchedFeature);

                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return Ok();
        }


        [HttpPost]
        public IActionResult Post([FromBody]Feature feature)
        {
            try
            {
                CreateDataFileIfNotExisted();
                var storedFeatures = GetStoredFeaturesInDataFile();
                
                // Add the passed feature to data file.
                storedFeatures.Add(feature);

                // Convert all stored features to json forman.
                var convertedJson = JsonSerializer.Serialize(storedFeatures);

                // Write stored data in json format to data file
                System.IO.File.WriteAllText(_dataFilePath, convertedJson);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(304, ex.Message);
            }
        }

        /// <summary>
        /// Converts Feature to FeatureResult objct
        /// </summary>
        private FeatureResult ToFeatureAccessibilityObject(Feature matchedFeature)
        {
            return new FeatureResult
            {
                CanAccess = matchedFeature.IsEnabled
            };
        }

        ///<summary>
        /// Initialized an empty data file, if no data file found.
        ///</summary>
        private void CreateDataFileIfNotExisted()
        {
            if (!System.IO.File.Exists(_dataFilePath))
            {
                using FileStream fileStream = System.IO.File.Create(_dataFilePath);
            }
        }

        /// <summary>
        /// Returns features stored in data file as a list of features
        /// </summary>
        private IList<Feature> GetStoredFeaturesInDataFile()
        {
            var data = string.Empty;
            using (StreamReader reader = new StreamReader(_dataFilePath))
            {
                var storedData = reader.ReadToEnd();
                data = string.IsNullOrEmpty(storedData) ? "[]" : storedData;
            }

            return JsonSerializer.Deserialize<List<Feature>>(data);
        }
    }

}
