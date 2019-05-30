using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Wigor.WeChat.PushSubscribeMessage.Models;

namespace Wigor.WeChat.PushSubscribeMessage.Core
{
    public class WeChatHelper
    {
        /// <summary>
        /// key 获取时间，value token
        /// </summary>
        static KeyValuePair<DateTime, string> _accessToken;
        readonly WeChatSettings _weChatSettings;

        readonly IWeChatHttpClient _httpClient;

        public WeChatHelper(IOptions<WeChatSettings> weChatSettingsOption, IWeChatHttpClient httpClient)
        {
            if (weChatSettingsOption?.Value == null)
            {
                throw new Exception("WeChatHelper 初始化失败，未在配置文件中找到关于 WeChatSettings 的配置");
            }

            if (string.IsNullOrEmpty(weChatSettingsOption.Value.GrantType)
                || string.IsNullOrEmpty(weChatSettingsOption.Value.Appid)
                || string.IsNullOrEmpty(weChatSettingsOption.Value.Secret)
            )
            {
                throw new Exception("WeChatHelper 初始化失败，配置缺失请检查（WeChatSettings）节点配置是否正确");
            }

            _httpClient = httpClient;

            _weChatSettings = weChatSettingsOption.Value;


        }

        /// <summary>
        /// 获取 access_token
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAccessToken()
        {
            if (_accessToken.Key > DateTime.Now && !string.IsNullOrEmpty(_accessToken.Value))
            {
                return _accessToken.Value;
            }

            var msg = await _httpClient.GetAsync($"/cgi-bin/token?grant_type={_weChatSettings.GrantType}&appid={_weChatSettings.Appid}&secret={_weChatSettings.Secret}");

            if (msg.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException($"获取 Token 失败，请求没有得到正确的响应状态，状态：{msg.StatusCode}！");

            string responseContent = await msg.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
                throw new Exception($"获取 Token 失败，返回的数据为空，响应状态码：{msg.StatusCode}");

            var resultInfo = JsonConvert.DeserializeObject<WeChatAccessTokenData>(responseContent);

            if (string.IsNullOrEmpty(resultInfo.AccessToken) || string.IsNullOrEmpty(resultInfo.ExpiresIn))
                throw new Exception($"获取 Token 失败，返回的数据为空，响应状态码：{msg.StatusCode}，数据：{responseContent}");

            _accessToken = new KeyValuePair<DateTime, string>
                (
                    DateTime.Now.AddSeconds(Convert.ToInt32(resultInfo.ExpiresIn)),
                    resultInfo.AccessToken
                );
            return resultInfo.AccessToken;
        }

        /// <summary>
        /// 获取素材信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="mediaId">素材ID</param>
        /// <returns></returns>
        public async Task<string> GetMaterial(string token, string mediaId)
        {
            string url = $"/cgi-bin/material/get_material?access_token={token}";
            string body = JsonConvert.SerializeObject(new { media_id = mediaId });
            HttpContent httpContent = new StringContent(body);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var msg = await _httpClient.PostAsync(url, httpContent);

            if (msg.StatusCode != System.Net.HttpStatusCode.OK)
                throw new HttpRequestException($"获取 素材：{mediaId} 失败，请求没有得到正确的响应状态，状态：{msg.StatusCode}！");

            string responseContent = await msg.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseContent))
                throw new Exception($"获取 素材：{mediaId} 失败，返回的数据为空，响应状态码：{msg.StatusCode}");

            var resultInfo = JsonConvert.DeserializeObject<dynamic>(responseContent);

            if (resultInfo.errcode != null)
                throw new Exception($"获取 素材：{mediaId} 失败，验证失败。微信端返回消息：{responseContent}响应状态码：{msg.StatusCode}");

            return responseContent;
        }

    }
}
