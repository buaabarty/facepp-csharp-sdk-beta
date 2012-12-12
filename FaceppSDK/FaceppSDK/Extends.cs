using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

namespace FaceppSDK
{
    public static class Extends
    {
        #region 转换成URL参数
        /// <summary>
        /// 转换成URL参数
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<object, object> dictionary)
        {
            var sb = new StringBuilder();
            foreach (var key in dictionary.Keys)
            {
                var value = dictionary[key];
                if (value != null)
                {
                    sb.Append(key + "=" + value + "&");
                }
            }
            return sb.ToString().TrimEnd('&');
        }

        public static string ToQueryString(this IList<object> list, string key)
        {
            var sb = new StringBuilder();
            foreach (var val in list)
            {
                if (val != null)
                {
                    sb.Append(key + "=" + Uri.EscapeDataString(val.ToString()) + "&");
                }
            }
            return sb.ToString().TrimEnd('&').Substring(key.Length + 1);
        }
        #endregion

        #region Json/Xml转换对象
        public static T ToEntity<T>(this string str) where T : class
        {
            return ToEntity<T>(str, "json");
        }

        public static T ToEntity<T>(this string str, object format) where T : class
        {
            T t = default(T);
            switch (format.ToString())
            {
                case "xml":
                    {
                        t = XmlHelper.Deserialize<T>(str);
                        break;
                    }
                case "json":
                default:
                    {
                        t = JsonHelper.DeserializeToObj<T>(str);
                        break;
                    }
            }
            return t;
        }
        #endregion

        #region 格林威治时间→DateTime
        public static DateTime ToDateTime(this string time)
        {
            return DateTime.ParseExact(time, "ddd MMM dd HH:mm:ss zzz yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        #endregion

        #region 字符串格式化
        public static string ToFormat(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
        #endregion
    }

    public class XmlHelper
    {
        #region XML→T
        public static T Deserialize<T>(string xml)
        {
            if (typeof(T).IsGenericType)
            {
                return DeserializeToEntityList<T>(xml);
            }
            else
            {
                return DeserializeToEntity<T>(xml);
            }
        }
        #endregion

        #region XML→T(单例)
        private static T DeserializeToEntity<T>(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                T obj = (T)xs.Deserialize(reader);
                return obj;
            }
        }
        #endregion

        #region XML→T(列表)
        private static IList<T> DeserializeToList<T>(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            string nodeName = typeof(T).Name.ToLower();
            IList<T> list = new List<T>();
            foreach (XmlNode node in document.GetElementsByTagName(nodeName))
            {
                list.Add(Deserialize<T>(node.OuterXml));
            }
            return list;
        }

        private static T DeserializeToEntityList<T>(string xml)
        {
            var method = typeof(XmlHelper).GetMethod("DeserializeToList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).MakeGenericMethod(typeof(T).GetGenericArguments()[0]);
            return (T)method.Invoke(null, new object[] { xml });
        }
        #endregion

        #region T→XML
        public static string Serialize<T>(T obj)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }
        #endregion
    }

    public class JsonHelper
    {
        #region T→Json
        public static string SerializeToJson<T>(T t)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(ms, t);

                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                string p = @"\\/Date\((\d+)\+\d+\)\\/";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                Regex reg = new Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);
                //如果是数组

                if (jsonString.StartsWith("[") && jsonString.EndsWith("]"))
                {
                    string name = t.GetType().Name;
                    if (t is System.Collections.ArrayList)
                    {
                        string[] arr = name.Split(new string[] { "[]" }, StringSplitOptions.None);
                        if (arr.Length > 0) name = arr[0] + "s";
                    }
                    else if (t is System.Collections.IList)
                    {
                        System.Collections.IList list = (System.Collections.IList)t;
                        name = list[0].GetType().Name + "s";
                    }
                    jsonString = "{\"" + name + "\":" + jsonString + "}";
                }
                return jsonString;
            }
        }
        #endregion

        #region Json→T
        public static T DeserializeToObj<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            if (jsonString != null)
            {
                jsonString = reg.Replace(jsonString, matchEvaluator);
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                    T obj = (T)ser.ReadObject(ms);
                    return obj;
                }
            }
            else return default(T);
        }
        #endregion

        #region 时间转换
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        #endregion
    }
}
