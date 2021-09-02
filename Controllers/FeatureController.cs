using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using moneyLionAssignment.Models;
using Newtonsoft.Json;

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
                    var result = new object();
                    if (System.IO.File.Exists(_dataFilePath))
                    {
                        using (StreamReader reader = new StreamReader(_dataFilePath))
                        {
                            var data = reader.ReadToEnd();
                            var features = JsonConvert.DeserializeObject<List<Feature>>(data);
                            result = features.Where(feature => feature.FeatureName == featureName && feature.Email == email)
                                    .Select(f => new FeatureResult{ CanAccess = f.IsEnabled});
                        }

                        return Ok(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Feature feature)
        {
            try
            {
                var data = string.Empty;
                if (!System.IO.File.Exists(_dataFilePath))
                {
                    using FileStream fileStream = System.IO.File.Create(_dataFilePath);
                }
                using (StreamReader reader = new StreamReader(_dataFilePath))
                {
                    var storedData = reader.ReadToEnd();
                    data = string.IsNullOrEmpty(storedData) ? "[]" : storedData;
                    reader.Close();
                }
               
                    var features = JsonConvert.DeserializeObject<List<Feature>>(data);
                    features.Add(feature);
                    var convertedJson = JsonConvert.SerializeObject(features, Formatting.Indented);
                    System.IO.File.WriteAllText(_dataFilePath, convertedJson);

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return StatusCode(304);
            }
        }
    }

}
