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
        public int ProviderId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string Url { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
