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
        /// 计划信息展示 WF20151029 测试用例：U201510260001 PLN201510260001 20151028 20151039 Bloodpressure Bloodpressure_1
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PlanNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/PlanInfoChart")]
        [EnableQuery]
        public List<ComplianceAllSignsListByPeriod> GetComplianceAllSignsListByPeriod(string UserId, string PlanNo, int StartDate, int EndDate, string ItemType, string ItemCode)
        //public List<ComplianceAllSignsListByPeriod> GetComplianceAllSignsListByPeriod(string UserId, string PlanNo, int StartDate, int EndDate)
        {
            return repository.GetComplianceAllSignsListByPeriod(UserId, PlanNo, StartDate, EndDate, ItemType, ItemCode);
        }

        /// <summary>
        /// 根据主键删除Ps.Task任务表的一条数据 SYF 20151029
        /// </summary>
        /// <param name="Plan"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <param name="SortNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Task")]
        public HttpResponseMessage DeleteTask(List<DeleteTask> items)
        {
            int ret = 2;
            for (int i = 0; i < items.Count; i++)
            {
                ret = repository.DeteteTask(items[i].Plan, items[i].Type, items[i].Code, items[i].SortNo);
                if(ret!=1)
                {
                    break;
                }
            }
            return new ExceptionHandler().DeleteData(Request, ret);
        }

        /// <summary>
        /// Ps.Plan.SetData GL 2015-10-13
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Plan")]
        [ModelValidationFilter]
        public HttpResponseMessage PostPlan(GPlanInfo item)
        {
            int ret = repository.SetPlan(item.PlanNo, item.PatientId, Convert.ToInt32(item.StartDate), Convert.ToInt32(item.EndDate), item.Module, Convert.ToInt32(item.Status), item.DoctorId, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// Ps.ComplianceDetail.SetData CSQ 20151027
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/ComplianceDetail")]
        [ModelValidationFilter]
        public HttpResponseMessage PostComplianceDetail(ComplianceDetail item)
        {
            int ret = repository.SetComplianceDetail(item.PlanNo, item.Date, item.CategoryCode,  item.Code,item.SortNo, item.Status, item.Description,item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
   
       /// <summary>
       /// PostCreateTask 创建计划时插入Task信息 CSQ 20151025
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
        [Route("Api/v1/PlanInfo/Task")]
        [ModelValidationFilter]
        public HttpResponseMessage PostCreateTask(List<CreateTask> items)
        {
            int ret = 2;
            for (int i = 0; i < items.Count; i++)
            {
                ret = repository.CreateTask(items[i].PlanNo, items[i].Type, items[i].Code, items[i].SortNo, items[i].Instruction, items[i].piUserId, items[i].piTerminalName, items[i].piTerminalIP, items[i].piDeviceType);
                if(ret!=1)
                {
                    break;
                }
            }
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 获取某个计划下的所有任务 CSQ 20151025
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Tasks")]
        [EnableQuery]
        public List<PsTask> GetTasks(string PlanNo, string ParentCode, string Date, string PatientId)
        {
            return repository.GetTasks(PlanNo, ParentCode, Date, PatientId);
        }

        /// <summary>
        /// CSQ 20151026 获取任务子表数据
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/TaskDetails")]
        public List<TaskDetail> GetTaskDetails(string CategoryCode, string Code)
        {
            return repository.GetTaskDetails(CategoryCode, Code);
        }

        /// <summary>
        /// 插入专员计划模板信息 CSQ 20151027
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="DoctorId"></param>
        /// <param name="TemplateCode"></param>
        /// <param name="TemplateName"></param>
        /// <param name="Description"></param>
        /// <param name="RecordDate"></param>
        /// <param name="Redundance"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Template")]
        public HttpResponseMessage PostPsTemplateSetData(Template template)
        {
            int ret = repository.PsTemplateSetData(template.DoctorId, template.TemplateCode, template.TemplateName, template.Description, template.RecordDate, template.Redundance, template.piUserId, template.piTerminalName, template.piTerminalIP, template.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 专员建立计划模板 子表写数 csq 20151026
        /// </summary>
        /// <param name="templateDtl"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/TemplateDetail")]
        public HttpResponseMessage PostPsTemplateDetailSetData(TemplateDetail templateDtl)
        {
            int ret = repository.PsTemplateDetailSetData(templateDtl.DoctorId, templateDtl.TemplateCode, templateDtl.CategoryCode, templateDtl.ItemCode, templateDtl.Value, templateDtl.Description, templateDtl.Redundance, templateDtl.piUserId, templateDtl.piTerminalName, templateDtl.piTerminalIP, templateDtl.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 获取专员计划模板列表 CSQ 20151026
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="TemplateCode"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Templates")]
        public List<TemplateInfo> GetTemplateList(string DoctorId)
        {
            return repository.GetTemplateList(DoctorId);
        }

        /// <summary>
        /// 获取专员某个计划模板的详细信息 CSQ 20151026
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="TemplateCode"></param>
        /// <param name="ParentCode"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/TemplateDetails")]
        public List<TemplateInfoDtl> GetTemplateDetails(string DoctorId, string TemplateCode, string ParentCode)
        {
            return repository.GetTemplateDetails(DoctorId, TemplateCode, ParentCode);
        }

       /// <summary>
        /// Ps.Compliance.SetData CSQ 20151027
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
        [Route("Api/v1/PlanInfo/Compliance")]
        [ModelValidationFilter]
        public HttpResponseMessage PostCompliance(SetComplance item)
        {
            int ret = repository.SetCompliance(item.PlanNo, item.Date, item.Compliance, item.Description, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// Ps.Target.SetData CSQ 20151027 任务目标数据写入
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Target")]
        [ModelValidationFilter]
        public HttpResponseMessage PostTarget(TargetByCode item)
        {
            int ret = repository.SetTarget(item.Plan, item.Type, item.Code, item.Value, item.Origin, item.Instruction, item.Unit, item.piUserId, item.piTerminalName, item.piTerminalIP, item.piDeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// CSQ 获取某条任务的Target信息 20151027
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Type"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Target")]
        public HttpResponseMessage GetTarget(string PlanNo, string Type, string Code)
        {
            TargetByCode ret = repository.GetTarget(PlanNo, Type, Code);
            return new ExceptionHandler().toJson(ret);
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
        /// GetPlanInfo 根据PlanNo或PatientId/Module/Status,获取某计划详情 CSQ 20151031 
        /// 用法1：输入PlanNo，PatientId和Module为空，Status输入5 
        /// 用法2：PlanNo输入NULL，PatientId按界面输入，Module根据需要的模块输入，若为空则取全部模块数据；status根据需要输入2（未开始计划）/3（当前计划）/4（往前计划/已结束计划），若为0，则取全部状态的数据
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <returns></returns>
        [Route("Api/v1/PlanInfo/Plan")]
        public HttpResponseMessage GetPlanInfo(string PatientId, string PlanNo, string Module,int Status)
        {
            if (PlanNo!="NULL")
            {
                GPlanInfo ret = repository.GetPlanInfo(PlanNo);
                ret.PlanCompliance = repository.GetComplianceByPlanNo(PlanNo).ToString();
                return new ExceptionHandler().toJson(ret);
            }
            else
            {
                if(Module=="{Module}")
                {
                    Module = null;
                }
                List<GPlanInfo> ret = repository.GetPlanListByMS(PatientId, Module, Status);
                return new ExceptionHandler().toJson(ret);
            }
        }


        #region 暂时不用
        ///// <summary>
        ///// GetPatientDrugRecord 根据患者Id，获取药物治疗列表 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="Module"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/DrugRecords")]
        //[EnableQuery]
        //public List<PsDrugRecord> GetPatientDrugRecord(string PatientId, string Module)
        //{
        //    return repository.GetPatientDrugRecord(PatientId, Module);
        //}

        ///// <summary>
        ///// GetPlanInfobyPID 获取病人当前计划以及健康专员 "PlanNo|DoctorId" GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetPlanInfobyPID")]
        //public HttpResponseMessage GetPlanInfobyPID(string PatientId)
        //{
        //    string ret = repository.GetPlanInfobyPID(PatientId);
        //    return new ExceptionHandler().Common(Request, ret);
        //}

        ///// <summary>
        ///// GetImplementationByDate 通过某计划的日期，获取该天的任务完成详情 用于图上点点击时弹框内容 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="PlanNo"></param>
        ///// <param name="DateSelected"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetImplementationByDate")]
        //public TaskComDetailByD GetImplementationByDate(string PatientId, string PlanNo, string DateSelected)
        //{
        //    return repository.GetImplementationByDate(PatientId, PlanNo, DateSelected);
        //}

        ///// <summary>
        ///// GetSignInfoByCode 获取某体征的数据和画图信息（收缩压、舒张压、脉率) Pad和Phone都要用 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="PlanNo"></param>
        ///// <param name="ItemCode"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="EndDate"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetSignInfoByCode")]
        //public ChartData GetSignInfoByCode(string PatientId, string PlanNo, string ItemCode, int StartDate, int EndDate)
        //{
        //    return repository.GetSignInfoByCode(PatientId, PlanNo, ItemCode, StartDate, EndDate);
        //}

        ///// <summary>
        ///// GetImplementationForPadFirst 获取计划完成情况（Pad)-首次进入页面 PlanNo为空 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="Module"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetImplementationForPadFirst")]
        //public ImplementationInfo GetImplementationForPadFirst(string PatientId, string Module)
        //{
        //    return repository.GetImplementationForPadFirst(PatientId, Module);
        //}

        ///// <summary>
        ///// GetImplementationForPadSecond 获取计划完成情况（Pad)-查看往期计划 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="PlanNo"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetImplementationForPadSecond")]
        //public ImplementationInfo GetImplementationForPadSecond(string PatientId, string PlanNo)
        //{
        //    return repository.GetImplementationForPadSecond(PatientId, PlanNo);
        //}

        ///// <summary>
        ///// GetImplementationForPhone 获取计划完成情况（Phone)-查看当前计划近一周的情况 GL 2015-10-13 
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="Module"></param>
        ///// <returns></returns>    
        //[Route("Api/v1/PlanInfo/GetImplementationForPhone")]
        //public ImplementationPhone GetImplementationForPhone(string PatientId, string Module)
        //{
        //    return repository.GetImplementationForPhone(PatientId, Module);
        //}

        ///// <summary>
        ///// GetAllComplianceListByPeriod 获取某计划的某段时间(包括端点)的依从率列表 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="PlanNo"></param>
        ///// <param name="StartDate"></param>
        ///// <param name="EndDate"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetAllComplianceListByPeriod")]
        //[EnableQuery]
        //public List<ComplianceListByPeriod> GetAllComplianceListByPeriod(string PlanNo, int StartDate, int EndDate)
        //{
        //    return repository.GetAllComplianceListByPeriod( PlanNo, StartDate, EndDate);
        //}

        ///// <summary>
        ///// GetExecutingPlanByModule 根据模块获取正在执行的计划PlanNo CSQ 20151029
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="Module"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/ExecutingPlanByModule")]
        //public HttpResponseMessage GetExecutingPlanByModule(string PatientId, string Module)
        //{
        //    GPlanInfo ret = repository.GetExecutingPlanByModule(PatientId, Module);
        //    return new ExceptionHandler().toJson(ret);
        //}


        ///// <summary>
        ///// GetPlanList34ByM 获取某模块患者的正在执行的和结束的计划列表 GL 2015-10-13
        ///// </summary>
        ///// <param name="PatientId"></param>
        ///// <param name="Module"></param>
        ///// <returns></returns>
        //[Route("Api/v1/PlanInfo/GetPlanList34ByM")]
        //[EnableQuery]
        //public List<GPlanInfo> GetPlanList34ByM(string PatientId, string Module)
        //{
        //    return repository.GetPlanList34ByM(PatientId, Module);
        //}
        #endregion

      


    }
}
