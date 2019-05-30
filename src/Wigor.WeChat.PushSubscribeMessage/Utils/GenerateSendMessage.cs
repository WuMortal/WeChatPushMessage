using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wigor.WeChat.PushSubscribeMessage.Core;
using Wigor.WeChat.PushSubscribeMessage.Models;

namespace Wigor.WeChat.PushSubscribeMessage.Utils
{
    public class GenerateSendMessage
    {
        /// <summary>
        /// 生成发送文本消息 XML
        /// </summary>
        /// <param name="content">需要发送内容</param>
        /// <param name="toUserName">用户OpenId</param>
        /// <param name="fromUserName">发送者，管理员</param>
        /// <param name="dateTime">时间戳</param>
        /// <returns></returns>
        public static string GenerateSendTextMessage(string content, string toUserName, string fromUserName, string dateTime)
        {
            StringBuilder sb = new StringBuilder();

            return
                sb.Append($"<xml>")
                 .Append($"<ToUserName><![CDATA[{toUserName}]]></ToUserName>")
                 .Append($"<FromUserName><![CDATA[{fromUserName}]]></FromUserName>")
                 .Append($"<CreateTime>{dateTime}</CreateTime>")
                 .Append($"<MsgType><![CDATA[{WechatConstants.MESSAGE_TEXT}]]></MsgType>")
                 .Append($"<Content><![CDATA[{content}]]></Content> ")
                 .Append($"<MsgId>{dateTime}</MsgId>")
                 .Append($"</xml>").ToString();
        }

        /// <summary>
        /// 生成发送图文消息XML
        /// </summary>
        /// <param name="materialContent">素材内容</param>
        /// <param name="toUserName">用户OpenId</param>
        /// <param name="fromUserName">发送者，管理员</param>
        /// <param name="dateTime">时间戳</param>
        /// <returns></returns>
        public static string GenerateSendNewMessage(string materialContent, string toUserName, string fromUserName, string dateTime)
        {
            int articleCount = 7;
            StringBuilder sb = new StringBuilder();
            string articles = GenerateArticles(materialContent);

            return
                sb.Append($"<xml>")
                    .Append($"<ToUserName><![CDATA[{toUserName}]]></ToUserName>")
                    .Append($"<FromUserName><![CDATA[{fromUserName}]]></FromUserName>")
                    .Append($"<CreateTime>{dateTime}</CreateTime>")
                    .Append($"<MsgType><![CDATA[{WechatConstants.MESSAGE_NEWS}]]></MsgType>")
                    .Append($"<ArticleCount>{articleCount}</ArticleCount>")
                    .Append($"<Articles>{articles}</Articles>")
                    .Append($"<MsgId>{dateTime}</MsgId>")
                    .Append($"</xml>").ToString();
        }

        private static string GenerateArticles(string materialContent)
        {
            StringBuilder sb = new StringBuilder();

            var newsListStr = materialContent;
            if (string.IsNullOrEmpty(newsListStr))
                return "";

            dynamic newsList = JsonConvert.DeserializeObject<dynamic>(newsListStr).news_item;

            foreach (var news in newsList)
            {
                sb.Append($"<item><Title><![CDATA[{news.title}]]></Title>")
                    .Append($"<Description><![CDATA[{news.content}]]></Description>")
                    .Append($"<PicUrl><![CDATA[{news.thumb_url}]]></PicUrl>")
                    .Append($"<Url><![CDATA[{news.url}]]></Url></item>");
            }

            return sb.ToString();
        }
    }
}
