﻿namespace WebServer.Server.Routing.Contracts
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Http.Contracts;
    using Handlers;

    public interface IAppRouteConfig
    {
        IReadOnlyDictionary<HttpRequestMethod, IDictionary<string, RequestHandler>> Routes { get; }

        void Get(string route, Func<IHttpRequest, IHttpResponse> handler);

        void Post(string route, Func<IHttpRequest, IHttpResponse> handler);

        void AddRoute(string route, RequestHandler handler);
    }
}
