namespace Wigor.WeChat.PushSubscribeMessage.Models
{
    public class WeChatSettings
    {
        public string GrantType { get; set; }

        public string Appid { get; set; }

        public string Secret { get; set; }

        public string ApiDomain { get; set; }

        /// <summary>
        /// 需要发送的素材ID
        /// </summary>
        public string SendMediaId { get; set; }

        /// <summary>
        /// 服务器配置中的Token
        /// </summary>
        public string Token { get; set; }
    }
}
