using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class ProductCreate
    {
        [FunctionName("ProductCreate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = null;

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("request body", requestBody);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if(name != null) 
            {
                log.LogInformation("name", name);
                var product = Db.CreateProduct(name);
                var json = JsonConvert.SerializeObject(new{
                    product = product
                });
                return (ActionResult)new OkObjectResult(json);
            } 
            else {
                return new BadRequestObjectResult("Missing name in posted Body");
            }
        }
    }
}
