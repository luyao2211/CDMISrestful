using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.Models
{
    public interface IPlanInfoRepository
    {
        int SetPlan(string PlanNo, string PatientId, int StartDate, int EndDate, string Module, int Status, string DoctorId, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int SetComplianceDetail(string Parent, string Id, int Status, string CoUserId, string CoTerminalName, string CoTerminalIP, int CoDeviceType);
        List<LifeStyleDetail> GetLifeStyleDetail(string Module);
        List<PsTaskByType> GetPsTaskByType(string PlanNo, string Type);
        List<PsDrugRecord> GetPatientDrugRecord(string PatientId, string Module);
        int CreateTask(string PlanNo, string Task, string UserId, string TerminalName, string TerminalIP, int DeviceType);
        int SetCompliance(string PatientId, int Date, string PlanNo, Double Compliance, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        string GetValueByPlanNoAndId(string PlanNo, string Id);
        int SetTarget(string Plan, string Id, string Type, string Code, string Value, string Origin, string Instruction, string Unit, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        GPlanInfo GetPlanInfo(string PlanNo);
        TaskComDetailByD GetImplementationByDate(string PatientId, string PlanNo, string DateSelected);
        ChartData GetSignInfoByCode(string PatientId, string PlanNo, string ItemCode, int StartDate, int EndDate);
        int GetGoalValue(string PlanNo);
        string GetExecutingPlanByModule(string PatientId, string Module);
        int SetPlanStart(string PlanNo, int Status, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        ImplementationInfo GetImplementationForPadFirst(string PatientId, string Module);
        ImplementationInfo GetImplementationForPadSecond(string PatientId, string PlanNo);
        List<OverDuePlanDetail> GetOverDuePlanList(string DoctorId, string ModuleType);
        List<TasksByStatus> GetTaskByStatus(string PatientId, string PlanNo, int PiStatus);
        string GetPlanInfobyPID(string PatientId);
        ImplementationPhone GetImplementationForPhone(string PatientId, string Module);
        List<TasksByDate> GetTasksByIndate(string PatientId, int InDate, string PlanNo);
        List<PlanDeatil> GetPlanList34ByM(string PatientId, string Module);
        List<ComplianceListByPeriod> GetAllComplianceListByPeriod(string PatientId, string PlanNo, int StartDate, int EndDate);
    }
}