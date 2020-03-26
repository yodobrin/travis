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
using Azure.Storage.Blobs;

namespace drives
{
    public static class UpdateDetails
    {
        [FunctionName("UpdateDetails")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = Environment.GetEnvironmentVariable("FILEREPOSA");
            string containerName = Environment.GetEnvironmentVariable("REPO");
            string fileName = Environment.GetEnvironmentVariable("TRAVIS");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic ddata = JsonConvert.DeserializeObject(requestBody);

            // create some footprint
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);      
            string blobName = "travis" + Guid.NewGuid().ToString();
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            string messageBody = CreateRow(ddata);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(messageBody));

            await blobClient.UploadAsync(stream);

            // var response = new { data };     
        
            string responseMessage = JsonConvert.SerializeObject("שמרתי");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseMessage, Encoding.UTF8, "application/json")
            };
        }

        private static string CreateRow(dynamic ddata)
        {
            string origin =  ddata?.origin;
            string dest =  ddata?.dest;
            string route = ddata?.route;
            string id_num = ddata?.id_num;
            return $"{id_num},{origin},{dest},{route}";
        }
    }
}
