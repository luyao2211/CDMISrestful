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
     [WebApiTracker]
    [RESTAuthorizeAttribute]
    public class RiskInfoController : ApiController
    {
        static readonly IRiskInfoRepository repository = new RiskInfoRepository();
        DataConnection pclsCache = new DataConnection();

        /// <summary>
        /// 根据收缩压获取血压等级说明 LY 2015-10-13
        /// </summary>
        /// <param name="SBP"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/GetDescription")]
        public HttpResponseMessage GetDescription(int SBP)
        {
            string ret = repository.GetDescription(pclsCache, SBP);
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
            int ret = repository.SetRiskResult(pclsCache, Item.UserId, Item.AssessmentType, Item.AssessmentName, Item.AssessmentTime, Item.Result, Item.revUserId, Item.TerminalName, new CommonFunction().getRemoteIPAddress(), Item.DeviceType);
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
            string ret = repository.GetRiskResult(pclsCache, UserId, AssessmentType);
            return new ExceptionHandler().Common(Request, ret);
        }

        /// <summary>
        /// 获取高血压风险评估结果 SYF 2015-11-16
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/M1Risk")]
        public M1Risk GetM1Risk(string UserId)
        {
            return repository.GetM1Risk(pclsCache, UserId);
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
            List<PsTreatmentIndicators> ret = repository.GetPsTreatmentIndicators(pclsCache, UserId);
            return ret;
        }

        /// <summary>
        /// Ps.TreatmentIndicators  SetData WF 20151027
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/TreatmentIndicators")]

        [ModelValidationFilter]
        public HttpResponseMessage POSTPsTreatmentIndicatorsSetData(RiskResult Item)
        {
            int ret = repository.PsTreatmentIndicatorsSetData(pclsCache, Item.UserId, Item.SortNo, Item.AssessmentType, Item.AssessmentName, Item.AssessmentTime, Item.Result, Item.revUserId, Item.TerminalName, new CommonFunction().getRemoteIPAddress(), Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// Ps.Parameters  SetData WF20151027
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
         [Route("Api/v1/RiskInfo/PsParameters")]

        [ModelValidationFilter]
        public HttpResponseMessage PostPsParametersSetData(Parameters Item)
        {
            int ret = repository.PsParametersSetData(pclsCache, Item.Indicators, Item.Id, Item.Name, Item.Value, Item.Unit, Item.revUserId, Item.TerminalName, new CommonFunction().getRemoteIPAddress(), Item.DeviceType);
            return new ExceptionHandler().SetData(Request, ret);
        }

        /// <summary>
        /// 获取评估表Ps.Parameters的具体参数（对应一次评估）  GetParameters WF20151027
        /// </summary>
        /// <param name="Indicators"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/Parameters")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public List<Parameters> GetParameters(string Indicators)
        {
            List<Parameters> ret = repository.GetParameters(pclsCache, Indicators);
            return ret;
        }
        /// <summary>
        /// Ps.TreatmentIndicators GetMaxSortNo
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/GetMaxSortNo")]
        [ModelValidationFilter]
        [RESTAuthorizeAttribute]
        public HttpResponseMessage GetMaxSortNo(string UserId)
        {
            int ret = repository.GetMaxSortNo(pclsCache, UserId);
            return new ExceptionHandler().Common(Request,ret.ToString());
        }

         /// <summary>
         /// 获取高血压模块评估参数所需输入 SYF 20151117
         /// </summary>
         /// <param name="UserId"></param>
         /// <returns></returns>
        [Route("Api/v1/RiskInfo/M1RiskInput")]
        public M1RiskInput GetM1RiskInput(string UserId)
        {
            return repository.GetM1RiskInput(pclsCache, UserId);
        }

         /// <summary>
        /// 获取心衰模型评估的所有输入 SYF 20151117
         /// </summary>
         /// <param name="UserId"></param>
         /// <returns></returns>
        [Route("Api/v1/RiskInfo/M3RiskInput")]
        public M3RiskInput GetM3RiskInput(string UserId)
        {
            return repository.GetM3RiskInput(pclsCache, UserId);
        }
        
        /// <summary>
        /// 获取心衰风险评估结果 SYF 2015-11-17
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/RiskInfo/M3Risk")]
        public M3Risk GetM3Risk(string UserId)
        {
            return repository.GetM3Risk(pclsCache, UserId);
        }
    }
}
