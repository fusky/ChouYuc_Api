using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChouYuc_Api
{
    public class HttpHelper
    {

        public static string HttpGet(string Url, WebHeaderCollection header = null, bool isUA = true, bool AllowAutoRedirect = false)
        {
            try
            {
                string retString = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                if (header != null)
                {
                    request.Headers = header;
                }
                if (isUA)
                {
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Mobile Safari/537.36");
                }
                request.Method = "GET";
                request.AllowAutoRedirect = AllowAutoRedirect;
                //request.UserAgent = "Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(myResponseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("302"))
                {
                    return ex.Response.Headers["Location"].ToString();
                }
                throw ex;
            }
        }
        public static string HttpPost(string url, string postData, string ContentType = "text/html;charset=UTF-8", WebHeaderCollection header = null)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (header != null)
            {
                request.Headers = header;
            }
            request.Headers.Add("User-Agent", "Mozilla/5.0 (iPad; CPU OS 11_0 like Mac OS X) AppleWebKit/604.1.34 (KHTML, like Gecko) Version/11.0 Mobile/15A5341f Safari/604.1");
            request.Method = "POST";
            request.ContentType = ContentType;
            Stream newStream = request.GetRequestStream();//创建一个Stream,赋值时写入HttpWebRequest对象提供的一个stream里面

            byte[] pp = Encoding.UTF8.GetBytes(postData);
            newStream.Write(pp, 0, pp.Length);
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// 获取 url链接 参数名对应的值，需要特定格式
        /// </summary>
        /// <param name="url">url链接</param>
        /// <param name="parameter">参数名</param>
        /// <returns>对应参数值</returns>
        public static string GetUrlParameterValue(string url, string parameter)
        {
            var index = url.IndexOf("?");
            //判断是否携带参数
            if (index > -1)
            {
                //为了去掉问号
                index++;
                //截取 参数部分
                var targetUrl = url.Substring(index, url.Length - index);
                //按 '&' 分成N个数组
                string[] Param = targetUrl.Split('&');
                //循环匹配
                foreach (var parm in Param)
                {
                    //再按等号分组
                    var values = parm.Split('=');
                    //统一按小写 去匹配
                    if (values[0].ToLower().Equals(parameter.ToLower()))
                    {
                        //返回匹配成功的值
                        return values[1];
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取url字符串参数，返回参数值字符串
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="url">url字符串</param>
        /// <returns></returns>
        public static string GetQueryString(string name, string url)
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.Text.RegularExpressions.MatchCollection mc = re.Matches(url);
            foreach (System.Text.RegularExpressions.Match m in mc)
            {
                if (m.Result("$2").Equals(name))
                {
                    return m.Result("$3");
                }
            }
            return "";
        }

        public static string StringToMD5Hash(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
    }
}
