using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ServiceStack.Redis;

namespace CDMISrestful.Models
{
    public class ServiceRepository : IServiceRepository
    {
        public string sendSMS(string mobile, string smsType)
        {
            try
            {

                var Client = new RedisClient("127.0.0.1", 6379);
                var token = "849407bfab0cf4c1a998d3d6088d957b";
                var appId = "0593b3c52f7d4f8aa9f9055585407e16";
                var accountSid = "b839794e66174938828d1b8ea9c58412";
                var tplId = "14452";
                var Jsonstring1 = "templateSMS";
                var Jsonstring2 = "appId";
                var Jsonstring3 = "param";
                var Jsonstring4 = "templateId";
                var Jsonstring5 = "to";
                var J6 = "{";

                Random rand = new Random();
                var randNum = rand.Next(100000, 1000000);
                var param = randNum + "," + 3;

                string JSONData = J6 + '"' + Jsonstring1 + '"' + ':' + '{' + '"' + Jsonstring2 + '"' + ':' + '"' + appId + '"' + ',' + '"' + Jsonstring3 + '"' + ':' + '"' + param + '"' + ',' + '"' + Jsonstring4 + '"' + ':' + '"' + tplId + '"' + ',' + '"' + Jsonstring5 + '"' + ':' + '"' + mobile + '"' + '}' + '}';

                if (mobile != "" && smsType != "")
                {
                    var Flag1 = Client.Get<string>(mobile + smsType);
                    if (Flag1 == null)
                    {
                        Client.Set<int>(mobile + smsType, randNum);
                        Client.Expire(mobile + smsType, 3 * 60);
                        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                        MD5 MD5 = MD5.Create();
                        var md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(accountSid + token + timestamp, "MD5").ToUpper();

                        System.Text.Encoding encode = System.Text.Encoding.ASCII;
                        byte[] bytedata = encode.GetBytes(accountSid + ":" + timestamp);
                        var authorization = Convert.ToBase64String(bytedata, 0, bytedata.Length);
                        var length1 = md5.Length;
                        var length2 = authorization.Length;

                        string Url = "https://api.ucpaas.com/2014-06-30/Accounts/" + accountSid + "/Messages/templateSMS?sig=" + md5;
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                        request.Method = "POST";
                        request.Accept = "application/json";
                        request.ContentType = "application/json;charset=utf-8";
                        request.Headers.Set("Authorization", authorization);
                        //request.ContentLength = 256;
                        //request.Headers.Set("content-type", "application/json;charset=utf-8");
                        byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
                        request.ContentLength = bytes.Length;
                        request.Timeout = 10000;
                        Stream reqstream = request.GetRequestStream();
                        reqstream.Write(bytes, 0, bytes.Length);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream streamReceive = response.GetResponseStream();
                        Encoding encoding = Encoding.UTF8;
                        StreamReader streamReader = new StreamReader(streamReceive, encoding);
                        string strResult = streamReader.ReadToEnd();
                        streamReceive.Dispose();
                        streamReader.Dispose();

                        return strResult;

                    }
                    else
                    {
                        var time = Client.Ttl(mobile + smsType);
                        string codeexist = "您的邀请码已发送，请等待" + time + "后重新获取";
                        return codeexist;
                    }
                }
                return null;
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }
                }
                return ex.Message;
            }
        }
    }
}