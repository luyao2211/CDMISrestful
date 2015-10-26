using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.Models;
using CDMISrestful.CommonLibrary;
using System.Web;

namespace CDMISrestful.Controllers
{
    [RESTAuthorizeAttribute]
    public class ServiceController : ApiController
    {
        static readonly IServiceRepository repository = new ServiceRepository();

        /// <summary>
        /// 发送验证码短信 20151016 CSQ
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="smsType"></param>
        /// <returns></returns>
        public HttpResponseMessage sendSMS(string phoneNo, string smsType)
        {
            string ret = repository.sendSMS(phoneNo, smsType);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 获取验证码 CSQ 20151021
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="smsType"></param>
        /// <param name="verification"></param>
        /// <returns></returns>
        public HttpResponseMessage checkverification(string mobile, string smsType, string verification)
        {
            string ret = repository.checkverification(mobile, smsType, verification).ToString();
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 推送 通知
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="Alias"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public HttpResponseMessage PushNotification(string platform, string Alias, string notification)
        {
            string ret = repository.PushNotification(platform, Alias, notification);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 获取远程调用的IP CSQ 20151026
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage getRemoteIPAddress()
        {
            string visitorIP = "";
            visitorIP = HttpContext.Current.Request.UserHostAddress;
            return new ExceptionHandler().Common(Request, visitorIP);
        }

    }
}
