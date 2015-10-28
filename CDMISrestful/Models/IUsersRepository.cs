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
        bool IsTokenValid(string token);
        ForToken LogOn(string PwType, string userId, string password, string role);
        //string IsUserValid(string userId, string password);

        int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int Activition(string UserId, string InviteCode,string role);
        int ChangePassword(string OldPassword, string NewPassword, string UserId, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        PatientsDataSet GetPatientsList(string DoctorId, string ModuleType, int Plan, int Compliance, int Goal);
        int Verification(string userId, string PwType);
        PatBasicInfo GetPatBasicInfo(string UserId);
        PatientDetailInfo GetPatientDetailInfo(string UserId);
        DocInfoDetail GetDoctorDetailInfo(string UserId);
    
        DoctorInfo GetDoctorInfo(string DoctorId);
        int SetDoctorInfoDetail(string Doctor, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int SetPsDoctor(string UserId, string UserName, int Birthday, int Gender, string IDNo, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
       
        int SetPatBasicInfo(string UserId, string UserName, int Birthday, int Gender, int BloodType, string IDNo, string DoctorId, string InsuranceType, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int SetPatBasicInfoDetail(string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType);


        string GetIDByInputPhone(string Type, string Name);
        List<Calendar> GetCalendar(string DoctorId);

        List<HealthCoachList> GetHealthCoachList();
        HealthCoachInfo GetHealthCoachInfo(string HealthCoachID);

        int ReserveHealthCoach(string DoctorId, string PatientId, string Module, string Description, int Status, DateTime ApplicationTime, DateTime AppointmentTime, string AppointmentAdd, string Redundancy, string revUserId, string TerminalName, string TerminalIP, int DeviceType);

        int UpdateReservation(string DoctorId, string PatientId, int Status, string revUserId, string TerminalName, string TerminalIP, int DeviceType);

        List<CommentList> GetCommentList(string DoctorId, string CategoryCode);

        List<HealthCoachListByPatient> GetHealthCoachListByPatient(string PatientId, string CategoryCode);

        int RemoveHealthCoach(string PatientId, string DoctorId, string CategoryCode);

        List<AppoitmentPatient> GetAppoitmentPatientList(string healthCoachID, string Status);

    }
}