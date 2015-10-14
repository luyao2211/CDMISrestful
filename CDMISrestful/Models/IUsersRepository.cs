using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;
using CDMISrestful.DataViewModels;

namespace CDMISrestful.Models
{
    public interface IUsersRepository
    {
        int LogOn(string PwType, string userId, string password, string role);
        string IsUserValid(string userId, string password);
        int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int Activition(string UserId, string InviteCode,string role);
        int ChangePassword(string OldPassword, string NewPassword, string UserId, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        PatientsDataSet GetPatientsList(string DoctorId, string ModuleType, int Plan, int Compliance, int Goal);
        int Verification(string userId, string PwType);
        PatBasicInfo GetPatBasicInfo(string UserId);
        PatientDetailInfo GetPatientDetailInfo(string UserId);
        PatientALLBasicInfo GetUserBasicInfo(string UserId);
        DocInfoDetail GetDoctorDetailInfo(string UserId);
        List<TypeAndName> GetTypeList(string Category);
        DoctorInfo GetDoctorInfo(string DoctorId);
        int SetDoctorInfoDetail(string Doctor, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int SetPsDoctor(string UserId, string UserName, int Birthday, int Gender, string IDNo, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        List<Insurance> GetInsuranceType();
        int SetPatBasicInfo(string UserId, string UserName, int Birthday, int Gender, int BloodType, string IDNo, string DoctorId, string InsuranceType, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
    
    }
}