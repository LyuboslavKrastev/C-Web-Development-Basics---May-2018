namespace SimpleMvc.App.Views.Users
{
    using Framework.Contracts;
    using System.IO;

    public class Register : IRenderable
    {
        public string Render()
        {
            var directory = Directory.GetCurrentDirectory();
            var page = File.ReadAllText($@"{directory}\ViewFiles\Users\Register.html");

            return page;
        }
    }
}
