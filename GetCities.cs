using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http;

namespace drives
{
    public static class GetCities
    {
        [FunctionName("GetCities")]        
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            // either load local/BLOB data file, or call SQL:
            List<Data> data = new List<Data>
            {
            new Data() { CityName = "ירושלים"},
            new Data() { CityName = "נהריה"},            
            };

            var response = new { data };     
        
            string json = JsonConvert.SerializeObject(response);


            return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
        }
    }

    class Data
    {
        public string CityName { get; set; }
    }
}
