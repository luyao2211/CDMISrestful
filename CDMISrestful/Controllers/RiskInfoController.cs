using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
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
    public class RiskInfoController : ApiController
    {
        static readonly IRiskInfoRepository repository = new RiskInfoRepository();
        /// <summary>
        /// 获取某计划下某任务的目标值 LY 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/Plan/{PlanNo}/Task/{Id}")]
        public HttpResponseMessage GetValueByPlanNoAndId(string PlanNo, string Id)
        {
            string ret = repository.GetValueByPlanNoAndId(PlanNo, Id);
            return new ExceptionHandler().Common(Request, ret);
        }


        /// <summary>
        /// 根据收缩压获取血压等级说明 LY 2015-10-13
        /// </summary>
        /// <param name="SBP"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/GetDescription")]
        public HttpResponseMessage GetDescription(int SBP)
        {
            string ret = repository.GetDescription(SBP);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 插入风险评估结果 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AssessmentType"></param>
        /// <param name="AssessmentName"></param>
        /// <param name="AssessmentTime"></param>
        /// <param name="Result"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>

        [Route("Api/v1/RiskInfo/RiskResult")]
        [ModelValidationFilter]
        public HttpResponseMessage PostRiskResult(RiskResult Item)
        {
            int ret = repository.SetRiskResult(Item.UserId, Item.AssessmentType, Item.AssessmentName, Item.AssessmentTime, Item.Result, Item.revUserId, Item.TerminalName, Item.TerminalIP, Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

     

        /// <summary>
        /// 根据UserId获取最新风险评估结果 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/RiskResult")]
        public HttpResponseMessage GetRiskResult(string UserId, string AssessmentType)
        {
            string ret = repository.GetRiskResult(UserId,  AssessmentType);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 获取风险评估所需输入 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/RiskInput")]
        public RiskInput GetRiskInput(string UserId)
        {
            return repository.GetRiskInput(UserId);
        }

        /// <summary>
        /// 根据UserId获取风险评估结果 WF 20151027
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/RiskResults")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public List<PsTreatmentIndicators> GetPsTreatmentIndicators(string UserId)
        {
            List<PsTreatmentIndicators> ret = repository.GetPsTreatmentIndicators(UserId);
            return ret;
        }
        /// <summary>
        /// Ps.TreatmentIndicators  SetData WF20151027
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/PsTreatmentIndicatorsSetData")]
        [ModelValidationFilter]
        public HttpResponseMessage POSTPsTreatmentIndicatorsSetData(RiskResult Item)
        {
            int ret = repository.PsTreatmentIndicatorsSetData(Item.UserId, Item.AssessmentType, Item.AssessmentName, Item.AssessmentTime, Item.Result, Item.revUserId, Item.TerminalName, Item.TerminalIP, Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        /// <summary>
        /// Ps.Parameters  SetData WF20151027
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/PsParametersSetData")]
        [ModelValidationFilter]
        public HttpResponseMessage POSTPsParametersSetData(Parameters Item)
        {
            int ret = repository.PsParametersSetData(Item.Indicators, Item.Id, Item.Name, Item.Value, Item.Unit, Item.revUserId, Item.TerminalName, Item.TerminalIP, Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }
        /// <summary>
        /// Ps.Parameters  GetParameters WF20151027
        /// </summary>
        /// <param name="Indicators"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/GetParameters")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public List<Parameters> GetParameters(string Indicators)
        {
            List<Parameters> ret = repository.GetParameters(Indicators);
            return ret;
        }
    }
}
