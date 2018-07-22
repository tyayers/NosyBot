using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NosyBot.Services.Dtos;

namespace NosyFrontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<WorldStoryRecords> Get([FromQuery] int count = 10, [FromQuery] string displayLanguage = "en")
        {
            NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

            WorldStoryRecords records = repo.GetRegionStories(count);

            TranslateCommand translate = new TranslateCommand() { DisplayLanguage = displayLanguage };

            // Now translate AMERICAS if necessary
            foreach (StoryRecord story in records.Americas)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
                    //story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }

            // EMEA
            foreach (StoryRecord story in records.EMEA)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
                    //story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }

            // APAC
            foreach (StoryRecord story in records.APAC)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
                    //story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }

            NosyBot.Services.Utilities.ServiceProxies.GetTranslations(translate);

            return records;
        }

        // GET api/news/5
        [HttpGet("{country}")]
        public ActionResult<List<StoryRecord>> Get(string country = "us", [FromQuery] int count = 10, [FromQuery] string displayLanguage = "en")
        {
            NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

            List<StoryRecord> records = repo.GetLatestStoriesForCountry(country, count);
            TranslateCommand translate = new TranslateCommand() { DisplayLanguage = displayLanguage };

            foreach (StoryRecord story in records)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            NosyBot.Services.Utilities.ServiceProxies.GetTranslations(translate);

            return records;
        }

        [Route("continents")]
        public ActionResult<ContinentStoryRecords> Continents([FromQuery] int count = 10, [FromQuery] string displayLanguage = "en")
        {
            NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

            ContinentStoryRecords records = repo.GetContinentStories(count);

            TranslateCommand translate = new TranslateCommand() { DisplayLanguage = displayLanguage };

            // Translate NA
            foreach (StoryRecord story in records.NorthAmerica)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            // Translate SA
            foreach (StoryRecord story in records.SouthAmerica)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            // Translate EU
            foreach (StoryRecord story in records.Europe)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            // Translate AS
            foreach (StoryRecord story in records.Asia)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            // Translate AF
            foreach (StoryRecord story in records.Africa)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            // Translate AU
            foreach (StoryRecord story in records.Australia)
            {
                story.TranslatedTitle = story.Title;
                if (story.Language != null && story.Language != displayLanguage)
                    translate.AddStory(story);
            }

            NosyBot.Services.Utilities.ServiceProxies.GetTranslations(translate);

            return records;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
