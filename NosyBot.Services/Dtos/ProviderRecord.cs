using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    [TableName("Providers")]
    [PrimaryKey("Id")]
    public class ProviderRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string RssUrl { get; set; }
        public string LogoUrl { get; set; }
        public DateTime LastPublished { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
