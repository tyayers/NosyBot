using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using NosyBot.Services.Dtos;

namespace NosyFunctions
{
    public static class CheckNewsTimer
    {
        [FunctionName("CheckNewsTimer")]
        public static void Run([TimerTrigger("0 */30 * * * *")]TimerInfo myTimer, TraceWriter log)
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
                        if (!repo.CheckIfStoryExists(story))
                        {
                            System.Diagnostics.Trace.TraceInformation($"Adding story {story.Title} from provider {story.ProviderName}");
                            repo.InsertStory(story);
                        }
                    }
                }
            }
        }
    }
}
