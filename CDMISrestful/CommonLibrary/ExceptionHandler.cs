using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.CommonLibrary
{
    public class ExceptionHandler
    {
        public HttpResponseMessage IsUserValid(string ret)
        {
            if (ret == "不合法用户")
            {
                //var response = Request.CreateResponse<bool>(HttpStatusCode.Created, operationResult);
                //string uri = Url.Link("DefaultApi", new { id = item });
                //response.Headers.Location = new Uri(uri);
                //return response;
                //return new HttpResponseMessage(HttpStatusCode.Created);
                var resp = new HttpResponseMessage(HttpStatusCode.NoContent);
                resp.Content = new StringContent(string.Format(ret));
                return resp;
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Content = new StringContent(string.Format(ret));
                return resp;
            }
        }

        public HttpResponseMessage SetData(int operationResult)
        {
            //2 数据库连接失败
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("数据库连接失败"));
            switch (operationResult)
            {
                case 1:
                    //数据插入成功
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("数据插入成功"));
                    break;
                case 0:
                    //数据插入失败
                    resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    resp.Content = new StringContent(string.Format("数据插入失败"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage DeleteData(int operationResult)
        {

            //3 数据库连接失败  //0 数据删除失败  
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("数据删除失败"));
            switch (operationResult)
            {
                case 1:
                    //数据删除成功
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("数据删除成功"));
                    break;
                case 2:
                    //数据未找到
                    resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                    resp.Content = new StringContent(string.Format("数据未找到"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage LogOn(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("登录失败"));
            switch (operationResult)
            {
                case 1:
                    //"已注册激活且有权限，登陆成功，跳转到主页";
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("登陆成功"));
                    break;
                case 2:
                    //"已注册激活 但没有权限";
                    resp = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    resp.Content = new StringContent(string.Format("没有权限"));
                    break;
                case 3:
                    //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    resp = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    resp.Content = new StringContent(string.Format("暂未激活"));
                    break;
                case 4:
                    //"用户不存在";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("用户不存在"));
                    break;
                case 5:
                    //"密码错误";
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("密码错误"));
                    break;                    
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage Register(int operationResult)
        {

            //
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("注册失败"));
            switch (operationResult)
            {
                case 1:
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("注册成功"));
                    break;
                case 2:
                    resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    resp.Content = new StringContent(string.Format("注册失败"));
                    break;
                case 3:
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("同一用户名的同一角色已经存在"));
                    break;
                case 4:
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("新建角色成功，密码与您已有账号一致"));
                    break;               
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage Activation(int operationResult)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("激活失败"));
            switch (operationResult)
            {
                case 1:               
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("激活成功"));
                    break;
                case 2:
                    resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    resp.Content = new StringContent(string.Format("激活失败"));
                    break;
                default:
                    break;
            }
            return resp;
        }

        public HttpResponseMessage ChangePassword(int operationResult)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            resp.Content = new StringContent(string.Format("修改密码失败"));
            switch (operationResult)
            {
                case 1:
                    resp = new HttpResponseMessage(HttpStatusCode.OK);
                    resp.Content = new StringContent(string.Format("修改密码成功"));
                    break;
                case 2:
                    resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    resp.Content = new StringContent(string.Format("修改密码失败"));
                    break;
                case 3:
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("旧密码错误，请输入正确的旧密码"));
                    break;
                case 4:
                    resp = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    resp.Content = new StringContent(string.Format("密码已过期，请联系管理员重置密码"));
                    break;              
                default:
                    break;
            }
            return resp;
        }

        //public HttpResponseMessage Common(SignDetailByP ret)
        //{
        //    var resp = new HttpResponseMessage(HttpStatusCode.OK);
        //    resp.Content = new JsonContent(ret);
        //    return resp;
        //}
    }
}
                       
        