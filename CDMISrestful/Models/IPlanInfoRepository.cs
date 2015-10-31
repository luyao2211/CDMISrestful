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
        int SetComplianceDetail(string PlanNo, int Date, string CategoryCode, string Code, string SortNo, int Status, string Description, string CoUserId, string CoTerminalName, string CoTerminalIP, int CoDeviceType);
      
        List<LifeStyleDetail> GetLifeStyleDetail(string Module);
        List<PsDrugRecord> GetPatientDrugRecord(string PatientId, string Module);
        int CreateTask(string PlanNo, string Type, string Code, string SortNo, string Instruction, string UserId, string TerminalName, string TerminalIP, int DeviceType);

        int SetCompliance(string PlanNo, int Date, Double Compliance, string Description, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int SetTarget(string Plan , string Type, string Code, string Value, string Origin, string Instruction, string Unit, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        GPlanInfo GetPlanInfo(string PlanNo);
        TaskComDetailByD GetImplementationByDate(string PatientId, string PlanNo, string DateSelected);
        ChartData GetSignInfoByCode(string PatientId, string PlanNo, string ItemCode, int StartDate, int EndDate);
        GPlanInfo GetExecutingPlanByModule(string PatientId, string Module);
        int SetPlanStart(string PlanNo, int Status, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        //ImplementationInfo GetImplementationForPadFirst(string PatientId, string Module);
        //ImplementationInfo GetImplementationForPadSecond(string PatientId, string PlanNo);
        List<OverDuePlanDetail> GetOverDuePlanList(string DoctorId, string ModuleType);
        //string GetPlanInfobyPID(string PatientId);
        //ImplementationPhone GetImplementationForPhone(string PatientId, string Module);

        List<GPlanInfo> GetPlanList34ByM(string PatientId, string Module);
        List<ComplianceListByPeriod> GetAllComplianceListByPeriod( string PlanNo, int StartDate, int EndDate);
        List<PsTask> GetTasks(string PlanNo, string ParentCode,string Date,string PatientId);
        List<TaskDetail> GetTaskDetails(string CategoryCode, string Code);
        int PsTemplateSetData(string DoctorId, int TemplateCode, string TemplateName, string Description, DateTime RecordDate, string Redundance, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        int PsTemplateDetailSetData(string DoctorId, int TemplateCode, string CategoryCode, string ItemCode, string Value, string Description, string Redundance, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType);
        List<TemplateInfo> GetTemplateList(string DoctorId);
        List<TemplateInfoDtl> GetTemplateDetails(string DoctorId, string TemplateCode, string ParentCode);
        TargetByCode GetTarget(string PlanNo, string Type, string Code);

        //List<ComplianceAllSignsListByPeriod> GetComplianceAllSignsListByPeriod(string UserId, string PlanNo, int StartDate, int EndDate);
        List<ComplianceAllSignsListByPeriod> GetComplianceAllSignsListByPeriod(string UserId, string PlanNo, int StartDate, int EndDate, string ItemType, string ItemCode);

        int DeteteTask(string Plan, string Type, string Code, string SortNo);
        double GetComplianceByPlanNo(string PlanNo);

      
        List<GPlanInfo> GetPlanListByMS(string PatientId, string Module, int Status);
        List<TasksForClick> GetTasksForClick(string PlanNo, string ParentCode, string Date);

    }
}