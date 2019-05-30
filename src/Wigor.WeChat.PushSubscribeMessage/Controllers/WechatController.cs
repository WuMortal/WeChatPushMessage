using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Senparc.Weixin.MP;
using Wigor.WeChat.PushSubscribeMessage.Core;
using Wigor.WeChat.PushSubscribeMessage.Models;
using Wigor.WeChat.PushSubscribeMessage.Utils;

namespace Wigor.WeChat.PushSubscribeMessage.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WechatController : ControllerBase
    {

        readonly string _token;
        readonly string _mediaId;
        readonly WeChatHelper _weChatHelper;

        public WechatController(IConfiguration configuration, IOptions<WeChatSettings> weChatSettingsOption, WeChatHelper weChatHelper)
        {
            _weChatHelper = weChatHelper;
            _token = weChatSettingsOption.Value.Token;
            _mediaId = weChatSettingsOption.Value.SendMediaId;
        }

        [HttpGet]
        public IActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, _token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }

            return Content("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, _token));
        }


        [HttpPost]
        public async Task<string> Post(string signature, string timestamp, string nonce, string echostr)
        {
            if (!CheckSignature.Check(signature, timestamp, nonce, _token))
            {
                // 校验失败,不会对此作任何处理
                return "";
            }
            var notifyContent = Request.Body.ToStr(Encoding.UTF8);
            var dict = XmlUtil.FromXml(notifyContent);

            if (dict["Event"].ToString() != WechatConstants.MESSAGE_SUBSCRIBE)
            {
                //不会对此作任何处理
                return "";
            }
            var toUserName = dict["FromUserName"].ToString();
            var fromUserName = dict["ToUserName"].ToString();
            var dateTime = dict["CreateTime"].ToString();

            var token = await _weChatHelper.GetAccessToken();
            var content = await _weChatHelper.GetMaterial(token, _mediaId);
            return GenerateSendMessage.GenerateSendNewMessage(content, toUserName, fromUserName, dateTime);
        }


    }
}