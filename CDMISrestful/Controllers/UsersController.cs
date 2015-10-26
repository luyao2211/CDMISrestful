using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.OData;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    //[AllowAnonymous]  
    public class UsersController : ApiController
    {
        static readonly IUsersRepository repository = new UsersRepository();

        /// <summary>
        /// 根据输入的手机号和邮箱等获取系统唯一标识符 20151023 CSQ
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/UID")]
        public HttpResponseMessage GetIDByInputPhone(string Type, string Name)
        {
            string ret = repository.GetIDByInputPhone(Type,Name);
            return new ExceptionHandler().Common(Request, ret);   
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="logOn"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/LogOn")]
        [ModelValidationFilter]
        public HttpResponseMessage LogOn(LogOn logOn)
        {
          
            //msg.url = "http://my.company.com/login";

            //if (SecurityManager.IsTokenValid(token))
            //{
            ForToken ret = new ForToken();
            ret = repository.LogOn(logOn.PwType, logOn.username, logOn.password, logOn.role);
            return new ExceptionHandler().LogOn(Request,ret);          
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
        [EnableQuery]
        [RESTAuthorizeAttribute]
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
        [RESTAuthorizeAttribute]
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
        [RESTAuthorizeAttribute]
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
        [RESTAuthorizeAttribute]
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
        [RESTAuthorizeAttribute]
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
        [Route("Api/v1/Users/DoctorDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
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
        [Route("Api/v1/Users/DoctorInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage SetPsDoctor(SetPsDoctor SetPsDoctor)
        {
            int ret = repository.SetPsDoctor(SetPsDoctor.UserId, SetPsDoctor.UserName, SetPsDoctor.Birthday, SetPsDoctor.Gender, SetPsDoctor.IDNo, SetPsDoctor.InvalidFlag, SetPsDoctor.piUserId, SetPsDoctor.piTerminalName, SetPsDoctor.piTerminalIP, SetPsDoctor.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        
        /// <summary>
        /// 插入患者基本信息
        /// </summary>
        /// <param name="SetPatBasicInfo"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/BasicInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage SetPatBasicInfo(SetPatBasicInfo SetPatBasicInfo)
        {
            int ret = repository.SetPatBasicInfo(SetPatBasicInfo.UserId, SetPatBasicInfo.UserName, SetPatBasicInfo.Birthday, SetPatBasicInfo.Gender, SetPatBasicInfo.BloodType, SetPatBasicInfo.IDNo, SetPatBasicInfo.DoctorId, SetPatBasicInfo.InsuranceType, SetPatBasicInfo.InvalidFlag, SetPatBasicInfo.piUserId, SetPatBasicInfo.piTerminalName, SetPatBasicInfo.piTerminalIP, SetPatBasicInfo.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 插入患者详细信息 LY 2015-10-14
        /// </summary>
        /// <param name="Patient"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="ItemSeq"></param>
        /// <param name="Value"></param>
        /// <param name="Description"></param>
        /// <param name="SortNo"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/BasicDtlInfo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage PostPatBasicInfoDetail(BasinInfoDetail Item)
        {
            int ret = repository.SetPatBasicInfoDetail(Item.Patient, Item.CategoryCode, Item.ItemCode, Item.ItemSeq, Item.Value, Item.Description, Item.SortNo, Item.revUserId, Item.TerminalName, Item.TerminalIP, Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        [Route("Api/v1/Users/GetCalendar")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public List<Calendar> GetCalendar(string DoctorId)
        {
            List<Calendar> ret = repository.GetCalendar(DoctorId);
            return ret;
        }
    }
}
