using System;

namespace Wigor.WeChat.PushSubscribeMessage.Core
{
    public class WechatConstants
    {
        //不同的消息类型
        public static readonly String MESSAGE_TEXT = "text";//文本消息
        public static readonly String MESSAGE_NEWS = "news";//图文消息
        public static readonly String MESSAGE_IMAGE = "image";//图片消息
        public static readonly String MESSAGE_VOICE = "voice";//语音消息
        public static readonly String MESSAGE_VIDEO = "video";//视频消息
        public static readonly String MESSAGE_SHORTVIDEO = "shortvideo";//小视频消息
        public static readonly String MESSAGE_MUSIC = "music";//音乐消息
        public static readonly String MESSAGE_LINK = "link";//链接消息
        public static readonly String MESSAGE_LOCATION = "location";//上传地理位置
        public static readonly String MESSAGE_EVENT = "event";//事件推送
        public static readonly String MESSAGE_SUBSCRIBE = "subscribe";//关注
        public static readonly String MESSAGE_UNSUBSCRIBE = "unsubscribe";//取消关注
        public static readonly String MESSAGE_CLICK = "CLICK";//菜单点击——点击菜单获取消息时触发click
        public static readonly String MESSAGE_VIEW = "VIEW";//点击菜单跳转链接时触发view
        public static readonly String MESSAGE_SCAN = "SCAN";//扫描带参数二维码——未关注时触发subscribe/已关注时触发scan
    }
}
