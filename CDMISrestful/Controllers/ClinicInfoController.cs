using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;
using CDMISrestful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CDMISrestful.Controllers
{
    [RESTAuthorizeAttribute]
    public class ClinicInfoController : ApiController
    {
        static readonly IClinicInfoRepository repository = new ClinicInfoRepository();

        /// <summary>
        /// 获取目前最新Num条临床数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AdmissionDate"></param>
        /// <param name="ClinicDate"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        [Route("Api/v1/ClinicInfo/GetClinicalNewMobile")]
        [ModelValidationFilter]
        
        public Clinic GetClinicalNewMobile(string UserId, DateTime AdmissionDate, DateTime ClinicDate, int Num)
        {
            Clinic ret = repository.GetClinicalNewMobile(UserId, AdmissionDate, ClinicDate, Num);
            return ret;
        }
        /// <summary>
        /// 获取临床大类信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Type"></param>
        /// <param name="VisitId"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [Route("Api/v1/ClinicInfo/GetClinicInfoDetail")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public ClinicInfoViewModel GetClinicInfoDetail(string UserId, string Type, string VisitId, string Date)
        {
            ClinicInfoViewModel ret = repository.GetClinicInfoDetail(UserId,Type, VisitId, Date);
            return ret;
        }
        /// <summary>
        /// 获取化验参数列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="VisitId"></param>
        /// <param name="SortNo"></param>
        /// <returns></returns>
        [Route("Api/v1/ClinicInfo/GetLabTestDtlList")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public List<LabTestDetails> GetLabTestDtlList(string UserId, string VisitId, string SortNo)
        {
            List<LabTestDetails> ret = repository.GetLabTestDtlList(UserId,  VisitId, SortNo);
            return ret;
        }
    }
}
