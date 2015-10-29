using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMISrestful.Models
{
    public interface IClinicInfoRepository
    {
        Clinic GetClinicalNewMobile(string UserId, DateTime AdmissionDate, DateTime ClinicDate, int Num);
        ClinicInfoViewModel GetClinicInfoDetail(string UserId, string Type, string VisitId, string Date);
        List<LabTestDetails> GetLabTestDtlList(string UserId, string VisitId, string SortNo);
        ClinicalInfoListViewModel GetClinicalInfoList(string UserId);
        string getLatestHUserIdByHCode(string UserId, string HospitalCode);
        List<SymptomsList> GetSymptomsList(string UserId, string VisitId);
        List<DiagnosisInfo> GetDiagnosisInfoList(string UserId, string VisitId);
        List<ExamInfo> GetExaminationList(string UserId, string VisitId);
        List<ExamDetails> GetExamDtlList(string UserId, string VisitId, string SortNo, string ItemCode);
        List<LabTestList> GetLabTestList(string UserId, string VisitId);
        List<DrugRecordList> GetDrugRecordList(string UserId, string VisitId);

    }
}
