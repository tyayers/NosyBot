using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    public class TranslateCommand
    {
        public string DisplayLanguage { get; set; }
        public Dictionary<string, Translate> Translations { get; set; } = new Dictionary<string, Translate>();

        public void AddStory(StoryRecord story)
        {
            if (!Translations.ContainsKey(story.Language))
            {
                Translations.Add(story.Language, new Translate() { LangaugeFrom = story.Language, LanguageTo = DisplayLanguage });                
            }

            Translations[story.Language].Stories.Add(story);
        }
    }

    public class Translate
    {
        public string LangaugeFrom { get; set; }
        public string LanguageTo { get; set; }
        public List<StoryRecord> Stories { get; set; } = new List<StoryRecord>();
    }
}
