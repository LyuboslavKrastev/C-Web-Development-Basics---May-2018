using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaApp.App.HtmlHelpers
{
    public class AdminIndexString
    {
        public static string TopMenuHtml = @"
                <ul class=""navbar-nav right-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                    </li>
                </ul>
               <ul class=""navbar-nav right-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/products/create"">Create Product</a>
                    </li>
                </ul>
                <ul class=""navbar-nav right-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/orders/all"">All Orders</a>
                    </li>
                </ul>
                <ul class=""navbar-nav left-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/users/logout"">Logout</a>
                    </li>
                </ul>";
    }
}
