using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace osu__Multi_Recommendation.Models
{
    public class MultiplayerModel
    {
        public long BeatmapID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string BeatmapView 
        { 
            get{return Artist + " - " + Title;} 
        }
        public string Version { get; set; }
        public string BeatmapsetHost { get; set; }
        public int PlayCount { get; set; }
    }
}
