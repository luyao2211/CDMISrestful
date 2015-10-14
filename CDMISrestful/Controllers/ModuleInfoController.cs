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
    public class ModuleInfoController : ApiController
    {
        static readonly IModuleInfoRepository repository = new ModuleInfoRepository();
        /// <summary>
        /// 输入PatientId和CategoryCode，获取患者已经购买的某个模块的详细信息 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/ChronicSurveys/{CategoryCode}/Items/GetItemInfoByPIdAndModule")]
        public List<PatBasicInfoDetail> GetItemInfoByPIdAndModule(string UserId, string CategoryCode)
        {
            return repository.GetItemInfoByPIdAndModule(UserId, CategoryCode);
        }

       

        /// <summary>
        /// 插入患者详细信息 LY 2015-10-14
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
        [Route("Api/v1/Users/{Patient}/ChronicSurveys/{CategoryCode}/Items/{ItemCode}/PostPatBasicInfoDetail")]
        public HttpResponseMessage PostPatBasicInfoDetail(string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = repository.SetPatBasicInfoDetail(Patient, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, revUserId, TerminalName, TerminalIP, DeviceType);
            return new ExceptionHandler().SetData(ret);
        }

        /// <summary>
        /// 获取模块关注详细信息 LY 2015-10-14
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/ChronicSurveys/{CategoryCode}/Items/GetMstInfoItemByCategoryCode")]
        public List<MstInfoItemByCategoryCode> GetMstInfoItemByCategoryCode(string CategoryCode)
        {
            return repository.GetMstInfoItemByCategoryCode(CategoryCode);
        }

        /// <summary>
        /// 获取同步患者购买模块下的某些信息 LY 2015-10-14
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/Users/{UserId}/ChronicSurveys/Items/GetSynBasicInfoDetail")]
        public SynBasicInfo GetSynBasicInfoDetail(string UserId)
        {
            return repository.SynBasicInfoDetail(UserId);
        }
    }
}
