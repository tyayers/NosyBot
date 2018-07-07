
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using NosyBot.Common.Dtos;
using System;
using NosyBot.Services.Dtos;

namespace NosyFunctions
{
    public static class CheckNews
    {
        [FunctionName("CheckNews")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

            StoryRecord u = new StoryRecord()
            {
                ProviderId = 1,
                LastUpdated = DateTime.Now,
                PublishDate = DateTime.Now,
                Title = "Second news story ever!"
            };

            repo.InsertStory(u);

            return new OkObjectResult("news checked.");

        }
    }
}
