using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.Models;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using System.Text;

namespace CDMISrestful.Controllers
{
     [WebApiTracker]
    [RESTAuthorizeAttribute]
    public class ServiceController : ApiController
    {
        static readonly IServiceRepository repository = new ServiceRepository();
        DataConnection pclsCache = new DataConnection();
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
        /// 浙大输出接口 LY 2015-10-29
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public HttpResponseMessage GetPatientInfo(string PatientId)
        {
            string ret = repository.GetPatientInfo(pclsCache, PatientId);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 浙大接收接口处理 LY 2015-10-31
        /// </summary>
        /// <param name="VitalSigns"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public HttpResponseMessage VitalSignFromZKY(VitalSignFromDevice VitalSigns, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            var HeaderList = Request.Headers.ToList();
            string HeaderContent = "";
            KeyValuePair<string, IEnumerable<string>> Header = HeaderList.Find(delegate(KeyValuePair<string, IEnumerable<string>> x)
            {
                return x.Key == "Token";
            });
            if (Header.Key != null)
                HeaderContent = Header.Value.First();
            if (HeaderContent != "#zjuBME319*")
                return new ExceptionHandler().SetData(Request, ret);
            ret = repository.VitalSignFromZKY(pclsCache, VitalSigns, revUserId, TerminalName, TerminalIP, DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
    }
}
