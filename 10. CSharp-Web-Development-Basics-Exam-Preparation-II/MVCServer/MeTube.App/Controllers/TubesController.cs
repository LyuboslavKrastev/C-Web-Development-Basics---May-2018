using MeTube.App.Attributes;
using MeTube.App.Models.BindingModels;
using MeTube.App.Models.ViewModels;
using MeTube.Models;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Interfaces;
using System.Linq;

namespace MeTube.App.Controllers
{
    public class TubesController : BaseController
    {
        [HttpGet]
        [AuthotirzeLogin]
        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        [AuthotirzeLogin]
        public IActionResult Upload(TubeUploadingBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.BuildErrorView();
                return this.View();
            }
            using (this.Context)
            {
                string youTubeId = GetYouTubeId(model.YouTubeLink);

                if (youTubeId == null)
                {
                    this.BuildErrorView();
                    return this.View();
                }

                var tube = new Tube()
                {
                    Title = model.Title,
                    Author = model.Author,
                    Description = model.Description,
                    YoutubeId = model.YouTubeLink,
                    UploaderId = this.DbUser.Id
                };

                this.Context.Tubes.Add(tube);
                this.Context.SaveChanges();

                return this.RedirectToAction($"/tubes/details?id={tube.Id}");
            }
        }

        [HttpGet]
        [AuthotirzeLogin]
        public IActionResult Details(int id)
        {
            using (this.Context)
            {
                var tube = this.Context.Tubes.Find(id);

                if (tube == null)
                {
                    this.Model.Data["error"] = "The requested video does not exist";
                    return this.RedirectToAction("/home/index");
                }

                tube.Views++;
                Context.SaveChanges();

                var model = new[] { tube }
                .Select(TubeDetailsViewModel.FromTube)
                .FirstOrDefault();

                var yotubeId = GetYouTubeId(model.YouTubeId);

                this.Model.Data["title"] = model.Title;
                this.Model.Data["youTubeId"] = yotubeId;
                this.Model.Data["author"] = model.Author;
                this.Model.Data["views"] = $"{model.Views.ToString()} " + (model.Views == 1? "View": "Views");
                this.Model.Data["description"] = model.Description;

                return this.View();
            }


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
