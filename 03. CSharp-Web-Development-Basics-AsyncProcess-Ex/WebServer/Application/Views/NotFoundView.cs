﻿namespace WebServer.Application.Views
{
    using Server.Contracts;

    public class NotFoundView : IView
    {
        public string View()
        {
            return "<h1>404 This page does not exist :/</h1>";
        }
    }
}

