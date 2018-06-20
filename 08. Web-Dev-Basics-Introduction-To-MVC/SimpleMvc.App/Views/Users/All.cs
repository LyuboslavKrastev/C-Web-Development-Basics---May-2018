namespace SimpleMvc.App.Views.Users
{
    using ViewModels;
    using System.Text;
    using Framework.Contracts.Generic;

    public class All : IRenderable<AllUsernamesViewModel>
    {
        public AllUsernamesViewModel Model { get; set; }

        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<a href=""/home/index"">Back to Home</a>")
            .AppendLine("<h3>All users</h3>")
            .AppendLine("<ul>");

            foreach (var username in Model.Usernames)
            {
                sb.AppendLine($@"<li><a href=""/users/profile?id={username.UserId}"">{username}</a></li>");
            }

            sb.AppendLine("</ul>");

            return sb.ToString();
        }
    }
}