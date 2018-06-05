using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace XamarinApproov
{
    public class ApproovInterceptor : DelegatingHandler
    {
        public static readonly string APPROOV_HEADER = "Approov-Token";
        string uri;

        public ApproovInterceptor(string uri)
        {
            this.uri = uri;
            InnerHandler = new HttpClientHandler();
        }

        public ApproovInterceptor(HttpMessageHandler handler, string uri)
        {
            this.uri = uri;
            InnerHandler = handler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // fetch approov token
            string token = await Approover.Shared.fetchTokenAsync(null);

            // add approov token header to request
            request.Headers.Add(APPROOV_HEADER, token);

            // pass down to inner handler
            var response = await base.SendAsync(request, cancellationToken);

            // pass up to outer handler
            return response;
        }
    }
}