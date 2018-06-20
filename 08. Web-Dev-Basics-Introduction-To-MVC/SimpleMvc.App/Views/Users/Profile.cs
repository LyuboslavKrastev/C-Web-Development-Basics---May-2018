namespace SimpleMvc.App.Views.Users
{
    using ViewModels;
    using Framework.Contracts.Generic;
    using System.Text;
    using WebServer.Common;

    public class Profile : IRenderable<UsersProfileViewModel>
    {
        public UsersProfileViewModel Model { get; set; }

        public string Render()
        {
            if (Model == null)
            {
                return new NotFoundView().View();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"<a href=""/home/index"">Back to Home</a>")
            .AppendLine($"<h2>User: {Model.Username}</h2>")
            .AppendLine("<form action=\"profile\" method=\"POST\">")
            .AppendLine("Title: <input type=\"text\" name=\"Title\" /></br>")
            .AppendLine("Content: <input type=\"text\" name=\"Content\" /></br>")
            .AppendLine($"<input type=\"hidden\" name=\"UserId\" value=\"{Model.UserId}\" />")
            .AppendLine("<input type=\"submit\" value=\"Add Note\" />")
            .AppendLine("</form>")
            .AppendLine("<h5>List of notes</h5>")
            .AppendLine("<ul>");

            foreach (var note in Model.Notes)
            {
                sb.AppendLine($"<li><strong>{note.Title}</strong> - {note.Content}</li>");
            }
            sb.AppendLine("</ul>");

            return sb.ToString();           
        }
    }
}
