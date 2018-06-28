using KittenApp.App.Models.BindingModels;
using KittenApp.App.Models.ViewModels;
using KittenApp.Models;
using Microsoft.EntityFrameworkCore;
using SimpleMvc.Framework.Attributes.Methods;
using SimpleMvc.Framework.Attributes.Security;
using SimpleMvc.Framework.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KittenApp.App.Controllers
{
    public class KittensController : BaseController
    {
        private readonly string AppDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).ToString();
        private const int KittensPerPage = 3;

        [HttpGet]
        [PreAuthorize]
        public IActionResult Add()
        {
            this.Model.Data["error"] = string.Empty;
            return this.View();
        }

        [HttpPost]
        [PreAuthorize]
        public IActionResult Add(KittenAddingModel model)
        {
            var breed = this.Context.Breeds
                .FirstOrDefault(br => br.Name == model.Breed);

            if (breed == null)
            {
                this.Model.Data["error"] = "Invalid breed";
                return this.View();
            }

            var kitten = new Kitten()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = breed
            };

            using (this.Context)
            {
                this.Context.Kittens.Add(kitten);
                this.Context.SaveChanges();
            }

            return this.RedirectToAction("/kittens/all");
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult All()
        {
            
            var kittens = this.Context.Kittens
                .Include(k => k.Breed)
                .Select(KittenViewModel.FromKitten)
                .ToList();

            StringBuilder result = GetKittensHtml(kittens);

            this.Model.Data["kittens"] = result.ToString();
            return this.View();
        }

        private static StringBuilder GetKittensHtml(System.Collections.Generic.List<KittenViewModel> kittens)
        {
            StringBuilder result = new StringBuilder();

            result.Append(@"<div class=""row text-center"">");

            for (var i = 0; i < kittens.Count; i++)
            {
                var kitten = kittens[i];

                var picture = $"/Content/img/{kitten.Breed}.jpg";

                if (picture.Contains("Street") || picture.Contains("American"))
                {
                    picture = picture.Replace(" ", "-");
                }

                result.AppendLine($@"<div class=""col-4"">
                        <img class=""img-thumbnail"" src=""{picture}"" alt=""{kitten.Name}'s photo"" />
                        <div>
                            <h5>Name: {kitten.Name}</h5>
                            <h5>Age: {kitten.Age}</h5>
                            <h5>Breed: {kitten.Breed}</h5>
                        </div>
                    </div>");

                if (i % KittensPerPage == KittensPerPage - 1)
                {
                    result.Append(@"</div><div class=""row text-center"">");
                }
            }

            return result;
        }
    }
}
