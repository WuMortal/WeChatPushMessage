using Newtonsoft.Json;

namespace Wigor.WeChat.PushSubscribeMessage.Models
{
    public class WeChatAccessTokenData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
