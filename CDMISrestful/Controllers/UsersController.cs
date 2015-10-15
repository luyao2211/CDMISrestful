using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    [RESTAuthorize]
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logOn"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/LogOn")]
        [ModelValidationFilter]
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
            return new ExceptionHandler().LogOn(Request,ret);
           
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
            return new ExceptionHandler().IsUserValid(Request, ret);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="Register"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Register")]
        [ModelValidationFilter]
        public HttpResponseMessage Register(Register Register)
        {
            int ret = repository.Register(Register.PwType, Register.userId, Register.UserName, Register.Password, Register.role, Register.revUserId, Register.TerminalName, Register.TerminalIP, Register.DeviceType);
            return new ExceptionHandler().Register(Request, ret);
        }
        /// <summary>
        /// 激活
        /// </summary>
        /// <param name="activation"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Activition")]
        [ModelValidationFilter]
        
        public HttpResponseMessage Activition(Activation activation)
        {
            int ret = repository.Activition(activation.UserId, activation.InviteCode, activation.role);
            return new ExceptionHandler().Activation(Request, ret);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ChangePassword"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/ChangePassword")]
        [ModelValidationFilter]
        public HttpResponseMessage ChangePassword(ChangePassword ChangePassword)
        {
            int ret = repository.ChangePassword(ChangePassword.OldPassword, ChangePassword.NewPassword, ChangePassword.UserId, ChangePassword.revUserId, ChangePassword.TerminalName, ChangePassword.TerminalIP, ChangePassword.DeviceType);
            return new ExceptionHandler().ChangePassword(Request, ret);
        }
        /// <summary>
        /// 获取健康专员负责的患者列表
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="ModuleType"></param>
        /// <param name="Plan"></param>
        /// <param name="Compliance"></param>
        /// <param name="Goal"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/GetPatientsList")]
        [ModelValidationFilter]
        
        //public PatientsDataSet GetPatientsList(GetPatientsList GetPatientsList)
        public PatientsDataSet GetPatientsList(string DoctorId, string ModuleType, int Plan, int Compliance,int Goal)
        {
            PatientsDataSet ret = repository.GetPatientsList(DoctorId, ModuleType, Plan, Compliance, Goal);
            return ret;
        }
        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="Verification"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Verification")]
        [ModelValidationFilter]
        public HttpResponseMessage Verification(Verification Verification)
        {
            int ret = repository.Verification(Verification.userId, Verification.PwType);
            return new ExceptionHandler().Verification(Request, ret);
        }

        /// <summary>
        /// 获取患者基本信息、模块信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/BasicInfo")]
        [ModelValidationFilter]
        public PatBasicInfo GetPatBasicInfo(string UserId)
        {
            PatBasicInfo ret = repository.GetPatBasicInfo(UserId);
            return ret;
        }
        /// <summary>
        /// 根据用户名获取用户详细信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/BasicDtlInfo")]
        [ModelValidationFilter]
        public PatientDetailInfo GetPatientDetailInfo(string UserId)
        {
            PatientDetailInfo ret = repository.GetPatientDetailInfo(UserId);
            return ret;
        }
        /// <summary>
        /// 根据用户名获取医生身份信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/DoctorDtlInfo")]
        [ModelValidationFilter]
        public DocInfoDetail GetDoctorDetailInfo(string UserId)
        {
            DocInfoDetail ret = repository.GetDoctorDetailInfo(UserId);
            return ret;
        }

        /// <summary>
        /// 根据用户名获取医生基本信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/DoctorInfo")]
        [ModelValidationFilter]
        public DoctorInfo GetDoctorInfo(string UserId)
        {
            DoctorInfo ret = repository.GetDoctorInfo(UserId);
            return ret;
        }
        /// <summary>
        /// Ps.DoctorInfoDetail写数据
        /// </summary>
        /// <param name="SetDoctorInfoDetail"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/SetDoctorInfoDetail")]
        [ModelValidationFilter]
        public HttpResponseMessage SetDoctorInfoDetail(SetDoctorInfoDetail SetDoctorInfoDetail)
        {
            int ret = repository.SetDoctorInfoDetail(SetDoctorInfoDetail.Doctor, SetDoctorInfoDetail.CategoryCode, SetDoctorInfoDetail.ItemCode, SetDoctorInfoDetail.ItemSeq, SetDoctorInfoDetail.Value, SetDoctorInfoDetail.Description, SetDoctorInfoDetail.SortNo, SetDoctorInfoDetail.piUserId, SetDoctorInfoDetail.piTerminalName, SetDoctorInfoDetail.piTerminalIP, SetDoctorInfoDetail.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        /// <summary>
        /// Ps.DoctorInfo写数据
        /// </summary>
        /// <param name="SetPsDoctor"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/SetPsDoctor")]
        [ModelValidationFilter]
        public HttpResponseMessage SetPsDoctor(SetPsDoctor SetPsDoctor)
        {
            int ret = repository.SetPsDoctor(SetPsDoctor.UserId, SetPsDoctor.UserName, SetPsDoctor.Birthday, SetPsDoctor.Gender, SetPsDoctor.IDNo, SetPsDoctor.InvalidFlag, SetPsDoctor.piUserId, SetPsDoctor.piTerminalName, SetPsDoctor.piTerminalIP, SetPsDoctor.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        /// <summary>
        /// GetInsuranceType
        /// </summary>
        /// <returns></returns>
        [Route("Api/v1/Users/GetInsuranceType")]
        [ModelValidationFilter]
        public List<Insurance> GetInsuranceType()
        {
            List<Insurance> ret = repository.GetInsuranceType();
            return ret;
        }
        /// <summary>
        /// 插入患者基本信息
        /// </summary>
        /// <param name="SetPatBasicInfo"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/SetPatBasicInfo")]
        [ModelValidationFilter]
        public HttpResponseMessage SetPatBasicInfo(SetPatBasicInfo SetPatBasicInfo)
        {
            int ret = repository.SetPatBasicInfo(SetPatBasicInfo.UserId, SetPatBasicInfo.UserName, SetPatBasicInfo.Birthday, SetPatBasicInfo.Gender, SetPatBasicInfo.BloodType, SetPatBasicInfo.IDNo, SetPatBasicInfo.DoctorId, SetPatBasicInfo.InsuranceType, SetPatBasicInfo.InvalidFlag, SetPatBasicInfo.piUserId, SetPatBasicInfo.piTerminalName, SetPatBasicInfo.piTerminalIP, SetPatBasicInfo.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
    }
}
