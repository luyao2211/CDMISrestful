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
    public class ClinicInfoController : ApiController
    {
        static readonly IClinicInfoRepository repository = new ClinicInfoRepository();

        [Route("Api/v1/ClinicInfo/GetClinicalNewMobile")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public Clinic GetClinicalNewMobile(GetClinicalNewMobile GetClinicalNewMobile)
        {
            Clinic ret = repository.GetClinicalNewMobile(GetClinicalNewMobile.UserId, GetClinicalNewMobile.AdmissionDate, GetClinicalNewMobile.ClinicDate, GetClinicalNewMobile.Num);
            return ret;
        }

        [Route("Api/v1/ClinicInfo/GetClinicInfoDetail")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public ClinicInfoViewModel GetClinicInfoDetail(GetClinicInfoDetail GetClinicInfoDetail)
        {
            ClinicInfoViewModel ret = repository.GetClinicInfoDetail(GetClinicInfoDetail.UserId, GetClinicInfoDetail.Type, GetClinicInfoDetail.VisitId, GetClinicInfoDetail.Date);
            return ret;
        }
        [Route("Api/v1/ClinicInfo/GetLabTestDtlList")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public List<LabTestDetails> GetLabTestDtlList(GetLabTestDtlList GetLabTestDtlList)
        {
            List<LabTestDetails> ret = repository.GetLabTestDtlList(GetLabTestDtlList.UserId,  GetLabTestDtlList.VisitId, GetLabTestDtlList.SortNo);
            return ret;
        }
    }
}
