using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using CDMISrestful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace CDMISrestful.Controllers
{
    [RESTAuthorizeAttribute]
    public class PlanInfoController : ApiController
    {
        static readonly IPlanInfoRepository repository = new PlanInfoRepository();

        /// <summary>
        /// Ps.Plan.SetData GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="PatientId"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="Module"></param>
        /// <param name="Status"></param>
        /// <param name="DoctorId"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Plan")]
        [ModelValidationFilter]
        public HttpResponseMessage PostPlan(GPlanInfo item)
        {
            int ret = repository.SetPlan(item.PlanNo, item.PatientId, Convert.ToInt32(item.StartDate), Convert.ToInt32(item.EndDate), item.Module, Convert.ToInt32(item.Status), item.DoctorId, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// Ps.ComplianceDetail.SetData GL 2015-10-13
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Id"></param>
        /// <param name="Status"></param>
        /// <param name="CoUserId"></param>
        /// <param name="CoTerminalName"></param>
        /// <param name="CoTerminalIP"></param>
        /// <param name="CoDeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/ComplianceDetail")]
        [ModelValidationFilter]
        public HttpResponseMessage PostComplianceDetail(ComplianceDetail item)
        {
            int ret = repository.SetComplianceDetail(item.Parent, item.Id, item.Status, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// GetLifeStyleDetail GL 2015-10-13
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetLifeStyleDetail")]
        [EnableQuery]
        public List<LifeStyleDetail> GetLifeStyleDetail(string Module)
        {
            return repository.GetLifeStyleDetail(Module);
        }

        /// <summary>
        /// GetPsTaskByType 根据Type(LifeStyle,VitalSign,Drug)获取某PlanNo的所有任务 GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetPsTaskByType")]
        [EnableQuery]
        public List<PsTaskByType> GetPsTaskByType(string PlanNo, string Type)
        {
            return repository.GetPsTaskByType(PlanNo, Type);
        }

        /// <summary>
        /// GetPatientDrugRecord 根据患者Id，获取药物治疗列表 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/DrugRecords")]
        [EnableQuery]
        public List<PsDrugRecord> GetPatientDrugRecord(string PatientId, string Module)
        {
            return repository.GetPatientDrugRecord(PatientId, Module);
        }

        /// <summary>
        /// PostCreateTask 创建计划（Task格式：Type1#Code1#Instruction1@Type2#Code2#Instruction2,如："LifeStyle#S001#减轻体重@LifeStyle#S002#锻炼",） GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Task"></param>
        /// <param name="UserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Task")]
        [ModelValidationFilter]
        public HttpResponseMessage PostCreateTask(CreateTask item)
        {
            int ret = repository.CreateTask(item.PlanNo, item.Task, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// Ps.Compliance.SetData GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Date"></param>
        /// <param name="PlanNo"></param>
        /// <param name="Compliance"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Compliance")]
        [ModelValidationFilter]
        public HttpResponseMessage PostCompliance(SetComplance item)
        {
            int ret = repository.SetCompliance(item.PatientId, item.Date, item.PlanNo, item.Compliance, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// GetValueByPlanNoAndId 获取某计划下某任务的目标值 GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetValueByPlanNoAndId")]
        public HttpResponseMessage GetValueByPlanNoAndId(string PlanNo, string Id)
        {
            string ret = repository.GetValueByPlanNoAndId(PlanNo, Id);
            return new ExceptionHandler().Common(Request,ret);
        }

        /// <summary>
        /// Ps.Target.SetData GL 2015-10-13
        /// </summary>
        /// <param name="Plan"></param>
        /// <param name="Id"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <param name="Value"></param>
        /// <param name="Origin"></param>
        /// <param name="Instruction"></param>
        /// <param name="Unit"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Target")]
        [ModelValidationFilter]
        public HttpResponseMessage PostTarget(TargetByCode item)
        {
            int ret = repository.SetTarget(item.Plan, item.Id, item.Type, item.Code, item.Value, item.Origin, item.Instruction, item.Unit, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// GetPlanInfo 根据PlanNo获取某计划详情 GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Plan")]
        public GPlanInfo GetPlanInfo(string PlanNo)
        {
            return repository.GetPlanInfo(PlanNo);
        }

        /// <summary>
        /// GetImplementationByDate 通过某计划的日期，获取该天的任务完成详情 用于图上点点击时弹框内容 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="PlanNo"></param>
        /// <param name="DateSelected"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetImplementationByDate")]
        public TaskComDetailByD GetImplementationByDate(string PatientId, string PlanNo, string DateSelected)
        {
            return repository.GetImplementationByDate(PatientId, PlanNo, DateSelected);
        }

        /// <summary>
        /// GetSignInfoByCode 获取某体征的数据和画图信息（收缩压、舒张压、脉率) Pad和Phone都要用 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="PlanNo"></param>
        /// <param name="ItemCode"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetSignInfoByCode")]
        public ChartData GetSignInfoByCode(string PatientId, string PlanNo, string ItemCode, int StartDate, int EndDate)
        {
            return repository.GetSignInfoByCode(PatientId, PlanNo, ItemCode, StartDate, EndDate);
        }

        /// <summary>
        /// GetGoalValue 获取当前血压跟目标血压之间的差值 GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetGoalValue")]
        public HttpResponseMessage GetGoalValue(string PlanNo)
        {
            string ret = repository.GetGoalValue(PlanNo).ToString();
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// GetExecutingPlanByModule 根据模块获取正在执行的计划PlanNo GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetExecutingPlanByModule")]
        public HttpResponseMessage GetExecutingPlanByModule(string PatientId, string Module)
        {
            string ret =  repository.GetExecutingPlanByModule(PatientId, Module);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// PostPlanStart 更新计划状态（开始计划） GL 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Status"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/PostPlanStart")]
        [ModelValidationFilter]
        public HttpResponseMessage PostPlanStart(GPlanInfo item)
        {
            int ret = repository.SetPlanStart(item.PlanNo, Convert.ToInt32(item.Status), item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// GetImplementationForPadFirst 获取计划完成情况（Pad)-首次进入页面 PlanNo为空 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetImplementationForPadFirst")]
        public ImplementationInfo GetImplementationForPadFirst(string PatientId, string Module)
        {
            return repository.GetImplementationForPadFirst(PatientId, Module);
        }

        /// <summary>
        /// GetImplementationForPadSecond 获取计划完成情况（Pad)-查看往期计划 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetImplementationForPadSecond")]
        public ImplementationInfo GetImplementationForPadSecond(string PatientId, string PlanNo)
        {
            return repository.GetImplementationForPadSecond(PatientId, PlanNo);
        }

        /// <summary>
        /// GetOverDuePlanList 获取健康专员负责的所有患者（最新结束但未达标的）计划列表 GL 2015-10-13
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="ModuleType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetOverDuePlanList")]
        [EnableQuery]
        public List<OverDuePlanDetail> GetOverDuePlanList(string DoctorId, string ModuleType)
        {
            return repository.GetOverDuePlanList(DoctorId, ModuleType);
        }

        /// <summary>
        /// GetTaskByStatus 在当天根据任务状态的完成情况输出相应的任务 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="PlanNo"></param>
        /// <param name="PiStatus"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetTaskByStatus")]
        [EnableQuery]
        public List<TasksByStatus> GetTaskByStatus(string PatientId, string PlanNo, int PiStatus)
        {
            return repository.GetTaskByStatus(PatientId, PlanNo, PiStatus);
        }

        /// <summary>
        /// GetPlanInfobyPID 获取病人当前计划以及健康专员 "PlanNo|DoctorId" GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetPlanInfobyPID")]
        public HttpResponseMessage GetPlanInfobyPID(string PatientId)
        {
            string ret =  repository.GetPlanInfobyPID(PatientId);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// GetImplementationForPhone 获取计划完成情况（Phone)-查看当前计划近一周的情况 GL 2015-10-13 
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>    
        [Route("Api/v1/PlanInfo/GetImplementationForPhone")]
        public ImplementationPhone GetImplementationForPhone(string PatientId, string Module)
        {
            return repository.GetImplementationForPhone(PatientId, Module);
        }

        /// <summary>
        /// GetTasksByIndate 根据计划编码和日期，获取任务 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="InDate"></param>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetTasksByIndate")]
        [EnableQuery]
        public List<TasksByDate> GetTasksByIndate(string PatientId, int InDate, string PlanNo)
        {
            return repository.GetTasksByIndate(PatientId, InDate, PlanNo);
        }

        /// <summary>
        /// GetPlanList34ByM 获取某模块患者的正在执行的和结束的计划列表 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetPlanList34ByM")]
        [EnableQuery]
        public List<PlanDeatil> GetPlanList34ByM(string PatientId, string Module)
        {
            return repository.GetPlanList34ByM(PatientId, Module);
        }

        /// <summary>
        /// GetAllComplianceListByPeriod 获取某计划的某段时间(包括端点)的依从率列表 GL 2015-10-13
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="PlanNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/GetAllComplianceListByPeriod")]
        [EnableQuery]
        public List<ComplianceListByPeriod> GetAllComplianceListByPeriod(string PatientId, string PlanNo, int StartDate, int EndDate)
        {
            return repository.GetAllComplianceListByPeriod(PatientId, PlanNo, StartDate, EndDate);
        }
    }
}
