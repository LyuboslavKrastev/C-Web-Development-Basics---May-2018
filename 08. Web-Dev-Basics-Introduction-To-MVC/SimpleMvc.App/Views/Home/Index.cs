namespace SimpleMvc.App.Views.Home
{
    using Framework.Contracts;
    using System.IO;

    public class Index : IRenderable
    {
        public string Render()
        {
            var directory = Directory.GetCurrentDirectory();
            var page = File.ReadAllText($@"{directory}\ViewFiles\Home\Index.html");

            return page;
        }
    }
}
