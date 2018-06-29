namespace NotesApp.Controllers
{
    using NotesApp.Models.ViewModels;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NotesController : BaseController
    {
        [HttpGet]
        public IActionResult All()
        {
            List<NoteViewModel> notes = this.Context
                .Notes
                .Select(note => new NoteViewModel
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var note in notes)
            {
                result
                    .Append($@"
                <div class=""card bg-light mb-3"" style=""max-width: 50%;"">
                 <div class=""card-body"">
               <h5 class=""card-title"">{note.Title}</h5>
                <p class=""card-text"">{note.Content}</p>
                 </div>
                </div>")
                    .AppendLine();
            }

            this.Model.Data["notes"] = result.ToString();

            return this.View();
        }
    }
}
