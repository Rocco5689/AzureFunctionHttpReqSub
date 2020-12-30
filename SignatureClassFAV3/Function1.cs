using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SignatureClassFAV3
{
    public static class Function1
    {
        public class MyModel
        {
            [JsonProperty("One")]
            public string One { get; set; }

            [JsonProperty("Two")]
            public List<MyModelExtension> Two { get; set; }
        }

        public class MyModelExtension
        {
            [JsonProperty("MyValue")]
            public string MyValue { get; set; }
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] MyModel myModel,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //var testResult = JsonConvert.DeserializeObject<MyModel[]>(myModel);

            string name = "name";

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(JsonConvert.SerializeObject(myModel).ToString());
        }
    }
}
