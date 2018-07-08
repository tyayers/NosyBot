using System;
using System.Collections.Generic;
using System.Text;

namespace NosyBot.Services.Dtos
{
    public class WorldStoryRecords
    {
        public List<StoryRecord> Americas { get; set; }
        public List<StoryRecord> EMEA { get; set; }
        public List<StoryRecord> APAC { get; set; }
    }
}
