using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    public class ServiceController : ApiController
    {
        static readonly IServiceRepository repository = new ServiceRepository();

        /// <summary>
        /// 发送验证码短信 20151016 CSQ
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="smsType"></param>
        /// <returns></returns>
        public string sendSMS(string phoneNo, string smsType)
        {
            return repository.sendSMS(phoneNo, smsType);
        } 
    }
}
