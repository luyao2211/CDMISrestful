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
        public Clinic GetClinicalNewMobile(string UserId, DateTime AdmissionDate, DateTime ClinicDate, int Num)
        {
            Clinic ret = repository.GetClinicalNewMobile(UserId, AdmissionDate, ClinicDate, Num);
            return ret;
        }

        [Route("Api/v1/ClinicInfo/GetClinicInfoDetail")]
        [ModelValidationFilter]

        //public HttpResponseMessage LogOn(string PwType, string username, string password, string role)
        public ClinicInfoViewModel GetClinicInfoDetail(string UserId, string Type, string VisitId, string Date)
        {
            ClinicInfoViewModel ret = repository.GetClinicInfoDetail(UserId,Type, VisitId, Date);
            return ret;
        }
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
