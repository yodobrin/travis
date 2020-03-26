/*
Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that. 
You agree: 
	(i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded;
    (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and
	(iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code
**/

// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
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
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
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
