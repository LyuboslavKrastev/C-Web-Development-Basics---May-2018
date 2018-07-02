using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaApp.App.HtmlHelpers
{
    public static class UnauthenticatedIndexString
    {
        public static string TopMenuHtml = @"<li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/login"">Login</a>
                        </li>
                        <li class=""nav-item"">
                            <a class=""nav-link nav-link-white"" href=""/users/register"">Register</a>
                        </li>";

        public static string IndexHtml = @"<div class=""jumbotron mt-3 chushka-bg-color"">
                        <h1>Welcome to Chushka Universal Web Shop</h1>
                        <hr class=""bg-white"" />
                        <h3><a class=""nav-link-dark"" href=""/users/login"">Login</a> if you have an account.</h3>
                        <h3><a class=""nav-link-dark"" href=""/users/register"">Register</a> if you don't.</h3>
                    </div>";
    }
}
