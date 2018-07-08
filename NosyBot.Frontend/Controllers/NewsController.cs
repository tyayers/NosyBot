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

            WorldStoryRecords records = repo.GetLatestStories(count);

            // Now translate AMERICAS if necessary
            foreach (StoryRecord story in records.Americas)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }
            // EMEA
            foreach (StoryRecord story in records.EMEA)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }
            // APAC
            foreach (StoryRecord story in records.APAC)
            {
                story.TranslatedTitle = story.Title;

                if (story.Language != null && story.Language != displayLanguage)
                    story.TranslatedTitle = NosyBot.Services.Utilities.ServiceProxies.GetTranslation(story.Title, story.Language, displayLanguage).Result;
            }
            return records;
        }

        //// GET api/values/5
        //[HttpGet("{count}")]
        //public ActionResult<WorldStoryRecords> Get(int count = 10)
        //{

        //    NosyBot.Services.Repositories.NosyRepository repo = new NosyBot.Services.Repositories.NosyRepository();

        //    WorldStoryRecords records = repo.GetLatestStories(count);

        //    return records;
        //}

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
