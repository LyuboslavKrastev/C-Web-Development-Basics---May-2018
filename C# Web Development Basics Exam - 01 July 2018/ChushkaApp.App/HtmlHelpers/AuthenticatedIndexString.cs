namespace ChushkaApp.App.HtmlHelpers
{
   public static class AuthenticatedIndexString
    {
        public static string TopMenuHtml = @"
                <ul class=""navbar-nav right-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                    </li>
                </ul>
                <ul class=""navbar-nav left-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/users/logout"">Logout</a>
                    </li>
                </ul>";

        public static string IndexHtml = string.Empty;
    }
}
