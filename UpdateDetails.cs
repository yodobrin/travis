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

using System;
using System.IO;
using System.Threading.Tasks;
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
            log.LogInformation("UpdateDetails function processed a request.");

            string connectionString = Environment.GetEnvironmentVariable("FILEREPOSA");
            string containerName = Environment.GetEnvironmentVariable("REPO");
            string fileName = Environment.GetEnvironmentVariable("TRAVIS");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic ddata = JsonConvert.DeserializeObject(requestBody);

            // create some footprint - usually will be saved to a DB 
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);      
            string blobName = "travis" + Guid.NewGuid().ToString();
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            string messageBody = CreateRow(ddata);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(messageBody));

            await blobClient.UploadAsync(stream);
        
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
