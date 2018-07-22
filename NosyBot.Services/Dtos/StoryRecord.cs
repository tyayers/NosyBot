using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    [TableName("Stories")]
    [PrimaryKey("Id")]
    public class StoryRecord
    {
        public int Id { get; set; }
        public string Url { get; set; } = "";
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        [ResultColumn]
        public string LogoUrl { get; set; }
        public string Title { get; set; }
        [Ignore]
        public string TranslatedTitle { get; set; } = "";
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        [ResultColumn]
        public string Country { get; set; }
        [ResultColumn]
        public string Continent { get; set; }
        public string Language { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
