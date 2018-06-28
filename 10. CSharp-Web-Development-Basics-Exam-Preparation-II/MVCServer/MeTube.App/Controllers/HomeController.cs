using MeTube.App.Models.ViewModels;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;
using System.Linq;
using System.Text;

namespace MeTube.App.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAuthenticated)
            {
                this.Model.Data["result"] =
               @"<div class=""jumbotron"">
                    <p class=""h1 display-3"">Welcome to MeTube&trade;!</p>
                    <p class=""h3"">The simplest, easiest to use, most comfortable Multimedia Application.</p>
                    <hr class=""my-3"">
                    <p><a href=""/users/login"">Login</a> if you have an account or <a href=""users/register"">Register</a> now and start tubing.</p>
                </div>";
            }
            else
            {
                var tubes = this.Context.Tubes
                    .Select(TubeIndexViewModel.FromTube)
                    .ToList();

                var tubesResult = new StringBuilder();

                tubesResult.Append(@"<div class=""row text-center"">");
                for (int i = 0; i < tubes.Count; i++)
                {
                    var tube = tubes[i];
                    var youtubeId = GetYouTubeId(tube.YouTubeId).Trim('"');

                    tubesResult.Append(
                        $@"<div class=""col-4"">
                            <img class=""img-thumbnail tube-thumbnail"" src=""https://img.youtube.com/vi/{youtubeId}/default.jpg"" alt=""{tube.Title}"" />
                            <div>
                                <h4><a href=""/tubes/details?id={tube.Id}"">{tube.Title}</a></td></h4>
                                <h5>{tube.Author}</h5>
                            </div>
                        </div>");

                    if (i % 3 == 2)
                    {
                        tubesResult.Append("</div>");
                        tubesResult.Append(@"<div class=""row text-center"">");
                    }
                }
                tubesResult.Append("</div>");

                this.Model.Data["result"] = tubesResult.ToString();
            }

            return this.View();
        }


        private static string GetYouTubeId(string youtubeLink)
        {
            if (youtubeLink.Contains("youtube.com"))
            {
                return youtubeLink.Split("?v=")[1];
            }
            else if (youtubeLink.Contains("youtu.be"))
            {
                return youtubeLink.Split("/").Last();
            }

            return null;
        }
    }
}
