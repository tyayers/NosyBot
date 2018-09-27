using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    public class TextAnalyticsRecord
    {
        public int StoryId { get; set; }
        public string Language { get; set; }
        public string Entities { get; set; }
        public decimal Sentiment { get; set; }
    }
}
