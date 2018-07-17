
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using NosyBot.Services.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml.Linq;

namespace NosyFunctions
{
    public static class CheckNews
    {
        [FunctionName("CheckNews")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

            List<ProviderRecord> providers = repo.GetProviders();

            foreach (ProviderRecord provider in providers)
            {
                List<StoryRecord> stories = NosyBot.Services.Utilities.RssUtilities.GetRSSUpdates(provider);
                if (stories.Count > 0)
                {
                    System.Diagnostics.Trace.TraceInformation($"Found {stories.Count} new stories from {provider.Name}");

                    // We have updates
                    repo.UpdateProvider(provider);

                    // Save to db
                    foreach (StoryRecord story in stories)
                    {
                        if (!repo.CheckIfStoryExists(story)) { 
                            System.Diagnostics.Trace.TraceInformation($"Adding story {story.Title} from provider {story.ProviderName}");
                            repo.InsertStory(story);
                        }
                    }
                }
            }

            return new OkObjectResult("news checked.");
        }
    }
}
