namespace WebServer.Server.Handlers
{
    using Common;
    using Contracts;
    using Server.Http;
    using Http.Contracts;
    using System;

    public abstract class RequestHandler : IRequestHandler
    {
        private const string ContentValue = "text/html";

        private readonly Func<IHttpRequest, IHttpResponse> handlingFunction;

        protected RequestHandler(Func<IHttpRequest, IHttpResponse> handlingFunction)
        {
            CoreValidator.ThrowIfNull(handlingFunction, nameof(handlingFunction));

            this.handlingFunction = handlingFunction;
        }

        public IHttpResponse Handle(IHttpContext httpContext)
        {
            var response = this.handlingFunction(httpContext.Request);

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(HttpHeader.ContentType, ContentValue);
            }

            return response;
        }
    }
}
