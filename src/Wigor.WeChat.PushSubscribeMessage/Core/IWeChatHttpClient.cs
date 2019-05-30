using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Wigor.WeChat.PushSubscribeMessage.Core
{
    public interface IWeChatHttpClient
    {
        Task<string> GetStringAsync(string requestUrl);
        Task<string> GetStringAsync(Uri requestUri);

        Task<HttpResponseMessage> GetAsync(string requestUrl);
        Task<HttpResponseMessage> GetAsync(Uri requestUri);

        Task<Stream> GetStreamAsync(string requestUrl);
        Task<Stream> GetStreamAsync(Uri requestUri);

        Task<HttpResponseMessage> PostAsync(string requestUrl, HttpContent content);
        Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);

        Task<HttpResponseMessage> PutAsync(string requestUrl, HttpContent content);
        Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage hrm);

        Task<HttpResponseMessage> DeleteAsync(string requestUrl);

        Task<HttpResponseMessage> DeleteAsync(Uri requestUri);
    }
}
