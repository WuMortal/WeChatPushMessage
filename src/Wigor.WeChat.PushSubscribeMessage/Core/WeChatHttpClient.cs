using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Wigor.WeChat.PushSubscribeMessage.Models;

namespace Wigor.WeChat.PushSubscribeMessage.Core
{
    public class WeChatHttpClient : IWeChatHttpClient
    {
        private readonly HttpClient _client;

        public WeChatHttpClient(HttpClient client, IOptions<WeChatSettings> weChatSettingsOption)
        {
            if (weChatSettingsOption?.Value == null)
            {
                throw new Exception("WeChatHelper 初始化失败，未在配置文件中找到关于 WeChatSettings 的配置");
            }

            if (string.IsNullOrEmpty(weChatSettingsOption.Value.ApiDomain))
            {
                throw new Exception("WeChatHelper 初始化失败，配置缺失请检查（WeChatSettings:ApiDomain）节点配置是否正确");
            }

            client.BaseAddress = new Uri(weChatSettingsOption.Value.ApiDomain);
            _client = client;
        }


        #region Implementation of IWechatHttpClient

        public async Task<string> GetStringAsync(string requestUrl)
        {
            return await _client.GetStringAsync(requestUrl);
        }

        public async Task<string> GetStringAsync(Uri requestUri)
        {
            return await _client.GetStringAsync(requestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            return await _client.GetAsync(requestUrl);
        }

        public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return await _client.GetAsync(requestUri);
        }

        public async Task<Stream> GetStreamAsync(string requestUrl)
        {
            return await _client.GetStreamAsync(requestUrl);
        }

        public async Task<Stream> GetStreamAsync(Uri requestUri)
        {
            return await _client.GetStreamAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUrl, HttpContent content)
        {
            return await _client.PostAsync(requestUrl, content);
        }

        public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        {
            return await _client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUrl, HttpContent content)
        {
            return await _client.PutAsync(requestUrl, content);
        }

        public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        {
            return await _client.PutAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage hrm)
        {
            return await _client.SendAsync(hrm);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUrl)
        {
            return await _client.DeleteAsync(requestUrl);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        {
            return await _client.DeleteAsync(requestUri);
        }
        #endregion
    }
}
