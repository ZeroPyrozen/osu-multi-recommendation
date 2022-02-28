using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using osu__Multi_Recommendation.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using CSharpOsu;
using CSharpOsu.Module;
using System.Threading;

namespace osu__Multi_Recommendation.Controllers
{
    public class MultiplayerController : Controller
    {

        // GET: Index
        public async Task<IActionResult> Index(int multiplayerID)
        {
            if (multiplayerID == 0)
            {
                return View("Error");
            }
            var popularBeatmapResult = GetPopularBeatmap(multiplayerID);
            if (popularBeatmapResult == null)
            {
                return View("Error");
            }
            return View(popularBeatmapResult);
        }

        private MultiplayerResultViewModel GetPopularBeatmap(int multiplayerID)
        {
            try
            {
                MultiplayerResultViewModel popularBeatmapResult = new MultiplayerResultViewModel();
                OsuClient osu = new OsuClient("APIKEY");
                OsuMatch match = osu.GetMatch(multiplayerID);
                popularBeatmapResult.matchTitle = match.match.name;
                int n = 0;
                popularBeatmapResult.matchCount = match.games.Count;
                
                for (int i = 0; i < match.games.Count; i++)
                {
                    var beatmapID = match.games[i].beatmap_id;
                    if (popularBeatmapResult.multiplayers.Count > 0)
                    {
                        var indexBeatmap = popularBeatmapResult.multiplayers.FindIndex(x => x.BeatmapID == beatmapID);
                        
                        if (indexBeatmap != -1)
                        {
                            popularBeatmapResult.multiplayers[indexBeatmap].PlayCount += 1;
                        }
                        else
                        {
                            popularBeatmapResult.multiplayers.Add(new MultiplayerModel()
                            {
                                BeatmapID = beatmapID,
                                Title = beatmapID.ToString(),
                                Artist = beatmapID.ToString(),
                                PlayCount = 1
                            });
                        }
                    }
                    else
                    {
                        popularBeatmapResult.multiplayers.Add(new MultiplayerModel()
                        {
                            BeatmapID = beatmapID,
                            Title = beatmapID.ToString(),
                            Artist = beatmapID.ToString(),
                            PlayCount = 1
                        });
                    }
                }
                var mostPlayedBeatmaps = popularBeatmapResult.multiplayers.OrderByDescending(x => x.PlayCount).Take(10).ToList();
                for(int i=0; i<mostPlayedBeatmaps.Count; i++)
                {
                    var beatmaps = osu.GetBeatmap(mostPlayedBeatmaps[i].BeatmapID, false);
                    if(beatmaps!=null && beatmaps.Length > 0)
                    {
                        mostPlayedBeatmaps[i].Artist = beatmaps[0].artist;
                        mostPlayedBeatmaps[i].Title = beatmaps[0].title;
                        mostPlayedBeatmaps[i].Version = beatmaps[0].difficulty;
                        mostPlayedBeatmaps[i].BeatmapsetHost = beatmaps[0].creator;
                    }
                }
                popularBeatmapResult.multiplayers = mostPlayedBeatmaps;
                return popularBeatmapResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
