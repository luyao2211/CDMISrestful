using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.Models;
using CDMISrestful.CommonLibrary;

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
    }
}
