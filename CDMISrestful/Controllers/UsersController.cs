using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    [RESTAuthorize]
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();

        [Route("Api/v1/Users/LogOn")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public HttpResponseMessage LogOn(LogOn logOn)
        {
            //string token = "";
            //WebRequest  headers = Request.Headers;  
            ////headers.get
            //string token = string.Format(Request.Headers[token]);

            //string token = WebRequest(Request.Headers["Range"]);

            // Create a new 'HttpWebRequest' Object to the mentioned URL.

            //string token = Request.Headers.GetValues[token];

            //HttpRequestMessage request = new HttpRequestMessage(LogOn, Uri);

            //HttpRequestMessage request = new HttpRequestMessage();
            //request.contentType = HTTPRequestMessage.CONTENT_TYPE_FORM;
            //msg.method = HTTPRequestMessage.POST_METHOD;
            //msg.url = "http://my.company.com/login";

            //if (SecurityManager.IsTokenValid(token))
            //{
            int ret = repository.LogOn(logOn.PwType, logOn.username, logOn.password, logOn.role);
            return new ExceptionHandler().LogOn(ret);
            //}
            //else
            //{
            //    //return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //   // return resp;
            //    var resp = new HttpResponseMessage(HttpStatusCode.NoContent);
            //    resp.Content = new StringContent(string.Format(token));
            //    return resp;
            //}
        }

        public HttpResponseMessage IsUserValid(String userId, String password)
        {
            string ret = repository.IsUserValid(userId, password);
            return new ExceptionHandler().IsUserValid(ret);
        }

        [Route("Api/v1/Users/Register")]
        [ModelValidationFilter]
        public HttpResponseMessage Register(Register Register)
        {
            int ret = repository.Register(Register.PwType, Register.userId, Register.UserName, Register.Password, Register.role, Register.revUserId, Register.TerminalName, Register.TerminalIP, Register.DeviceType);
            return new ExceptionHandler().Register(ret);
        }

        [Route("Api/v1/Users/Activition")]
        [ModelValidationFilter]
        
        public HttpResponseMessage Activition(Activation activation)
        {
            int ret = repository.Activition(activation.UserId, activation.InviteCode, activation.role);
            return new ExceptionHandler().Activation(ret);
        }

        [Route("Api/v1/Users/ChangePassword")]
        [ModelValidationFilter]
        public HttpResponseMessage ChangePassword(ChangePassword ChangePassword)
        {
            int ret = repository.ChangePassword(ChangePassword.OldPassword, ChangePassword.NewPassword, ChangePassword.UserId, ChangePassword.revUserId, ChangePassword.TerminalName, ChangePassword.TerminalIP, ChangePassword.DeviceType);
            return new ExceptionHandler().ChangePassword(ret);
        }
    }
}
