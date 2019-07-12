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
    public static class ProductGet
    {
        [FunctionName("ProductGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "ProductsGet/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var product = Db.GetProductById(id);
            var json = JsonConvert.SerializeObject(new {
                product = product
            });

            return (ActionResult) new OkObjectResult(json);
        }
    }
}
