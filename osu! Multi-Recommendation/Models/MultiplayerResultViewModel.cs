using CSharpOsu.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace osu__Multi_Recommendation.Models
{
    public class MultiplayerResultViewModel
    {
        public MultiplayerResultViewModel()
        {
            multiplayers = new List<MultiplayerModel>();
        }
        public string matchTitle { get; set; }
        public List<MultiplayerModel> multiplayers { get; set; }
        public List<OsuUser> players { get; set; }
        public int matchCount { get; set; }
    }
}
