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
    public static class GetRoutes
    {
        [FunctionName("GetRoutes")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetRoutes function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic ddata = JsonConvert.DeserializeObject(requestBody);
            string origin =  ddata?.origin;
            string dest =  ddata?.dest;
            
            // use a data file, or call SQL to obtain data
            List<Route> data = new List<Route>
            {
            new Route() { RouteName = "קריה דרך בגין, ראשון, צריפין"},
            new Route() { RouteName = "מרכזית המפרץ דרך עתלית לבונים"},            
            };
            
            

            var response = new { data };     
        
            string responseMessage = JsonConvert.SerializeObject(response);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseMessage, Encoding.UTF8, "application/json")
            };
        }
    }
    class Route
    {
        public string RouteName { get; set; }
    }
}
