using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OzgeErsu.Client.Core
{
    public class XHttpClientService : IXHttpClientService
    {
        public HttpClient BaseClient { get; }

        public Uri BaseAddress
        {
            get { return BaseClient.BaseAddress; }
            set { BaseClient.BaseAddress = value; }
        }
        public long MaxResponseContentBufferSize
        {
            get { return BaseClient.MaxResponseContentBufferSize; }
            set { BaseClient.MaxResponseContentBufferSize = value; }
        }
        public HttpRequestHeaders DefaultRequestHeaders => BaseClient.DefaultRequestHeaders;

        public TimeSpan Timeout
        {
            get { return BaseClient.Timeout; }
            set { BaseClient.Timeout = value; }
        }

        #region constructor
        private XHttpClientService(HttpClient baseClient)
        {
            BaseClient = baseClient;
        }

        private static readonly Lazy<XHttpClientService> Lazy =
            new Lazy<XHttpClientService>(
                () => new XHttpClientService(new HttpClient(new ModernHttpClient.NativeMessageHandler())));

        public static IXHttpClientService Instance => Lazy.Value;

        #endregion

        public void CancelPendingRequests()
        {
            BaseClient.CancelPendingRequests();
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return BaseClient.DeleteAsync(requestUri);
        }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            return BaseClient.DeleteAsync(requestUri);
        }
        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            return BaseClient.DeleteAsync(requestUri, cancellationToken);
        }
        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return BaseClient.DeleteAsync(requestUri, cancellationToken);
        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return BaseClient.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return BaseClient.GetAsync(requestUri);
        }
        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            return BaseClient.GetAsync(requestUri, cancellationToken);
        }
        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        {
            return BaseClient.GetAsync(requestUri, completionOption);
        }
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        {
            return BaseClient.GetAsync(requestUri, cancellationToken);
        }
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        {
            return BaseClient.GetAsync(requestUri, completionOption);
        }
        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return BaseClient.GetAsync(requestUri, completionOption, cancellationToken);
        }
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return BaseClient.GetAsync(requestUri, completionOption, cancellationToken);
        }


        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return BaseClient.PostAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            return BaseClient.PostAsync(requestUri, content);
        }
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return BaseClient.PostAsync(requestUri, content, cancellationToken);
        }
        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return BaseClient.PostAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        {
            return BaseClient.PutAsync(requestUri, content);
        }

        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            return BaseClient.PutAsync(requestUri, content);
        }
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return BaseClient.PutAsync(requestUri, content, cancellationToken);
        }
        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return BaseClient.PutAsync(requestUri, content, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return BaseClient.SendAsync(request);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return BaseClient.SendAsync(request, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
        {
            return BaseClient.SendAsync(request, completionOption);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return BaseClient.SendAsync(request, completionOption, cancellationToken);
        }

    }

}
