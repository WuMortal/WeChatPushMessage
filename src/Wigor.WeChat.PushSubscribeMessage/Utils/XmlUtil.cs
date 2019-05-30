using System.Collections.Generic;
using System.Xml;

namespace Wigor.WeChat.PushSubscribeMessage.Utils
{
    public class XmlUtil
    {
        /// <summary>
        /// 将xml转为WxData对象并返回对象内部的数据
        /// </summary>
        /// <param name="xml">待转换的xml串</param>
        /// <param name="appKey">string</param>
        /// <returns>经转换得到的Dictionary</returns>
        public static SortedDictionary<string, object> FromXml(string xml)
        {
            var sortedDict = new SortedDictionary<string, object>();

            if (string.IsNullOrEmpty(xml)) return sortedDict;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild; //获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                sortedDict[xe.Name] = xe.InnerText; 
            }

            return sortedDict;
        }
    }
}
