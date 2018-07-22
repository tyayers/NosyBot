using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    public class ContinentStoryRecords
    {
        public List<StoryRecord> NorthAmerica { get; set; }
        public List<StoryRecord> SouthAmerica { get; set; }
        public List<StoryRecord> Europe { get; set; }
        public List<StoryRecord> Asia { get; set; }
        public List<StoryRecord> Africa { get; set; }
        public List<StoryRecord> Australia { get; set; }
    }
}
