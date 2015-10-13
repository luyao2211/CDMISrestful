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
    public class RiskInfoController : ApiController
    {
        static readonly IRiskInfoRepository repository = new RiskInfoRepository();
        /// <summary>
        /// 获取某计划下某任务的目标值 LY 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Evaluations/GetValueByPlanNoAndId")]
        public string GetValueByPlanNoAndId(string PlanNo, string Id)
        {
            return repository.GetValueByPlanNoAndId(PlanNo, Id);
        }

        /// <summary>
        /// 获取血压等级字典表的所有信息 LY 2015-10-13
        /// </summary>
        /// <returns></returns>
        [Route("Api/v1/Users/Evaluations/GetBPGrades")]
        public List<MstBloodPressure> GetBPGrades()
        {
            return repository.GetBPGrades();
        }

        /// <summary>
        /// 根据收缩压获取血压等级说明 LY 2015-10-13
        /// </summary>
        /// <param name="SBP"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/Evaluations/GetDescription")]
        public string GetDescription(int SBP)
        {
            return repository.GetDescription(SBP);
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
        [Route("Api/v1/Users/{UserId}/Evaluations/PostRiskResult")]
        public HttpResponseMessage PostRiskResult(string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = repository.SetRiskResult(UserId, AssessmentType, AssessmentName, AssessmentTime, Result, revUserId, TerminalName, TerminalIP, DeviceType);
            return new ExceptionHandler().SetData(ret);
        }

        /// <summary>
        /// 修改用户详细信息 LY 2015-10-13
        /// </summary>
        /// <param name="Patient"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="ItemSeq"></param>
        /// <param name="Value"></param>
        /// <param name="Description"></param>
        /// <param name="SortNo"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{Patient}/Evaluations/PutBasicInfoDetail")]
        public HttpResponseMessage PutBasicInfoDetail(string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = repository.SetBasicInfoDetail(Patient, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, revUserId, TerminalName, TerminalIP, DeviceType);
            return new ExceptionHandler().SetData(ret);
        }

        /// <summary>
        /// 根据UserId获取最新风险评估结果 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/Evaluations/GetRiskResult")]
        public string GetRiskResult(string UserId)
        {
            return repository.GetRiskResult(UserId);
        }

        /// <summary>
        /// 获取风险评估所需输入 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/Evaluations/GetRiskInput")]
        public RiskInput GetRiskInput(string UserId)
        {
            return repository.GetRiskInput(UserId);
        }
    }
}
