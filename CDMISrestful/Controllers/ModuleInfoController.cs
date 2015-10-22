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
    public class ModuleInfoController : ApiController
    {
        static readonly IModuleInfoRepository repository = new ModuleInfoRepository();
        /// <summary>
        /// 输入PatientId和CategoryCode，获取患者已经购买的某个模块的详细信息 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        [Route("Api/v1/ModuleInfo/{UserId}/{CategoryCode}")]
        public List<PatBasicInfoDetail> GetItemInfoByPIdAndModule(string UserId, string CategoryCode)
        {
            return repository.GetItemInfoByPIdAndModule(UserId, CategoryCode);
        }

        ///// <summary>
        ///// 获取模块关注详细信息 LY 2015-10-14 CSQ 20151015注释，这个方法前端应该不会再用，可用GetItemInfoByPIdAndModule方法替代
        ///// </summary>
        ///// <param name="CategoryCode"></param>
        ///// <returns></returns>
        //[Route("Api/v1/ModuleInfo/{CategoryCode}/Items/GetMstInfoItemByCategoryCode")]
        //public List<MstInfoItemByCategoryCode> GetMstInfoItemByCategoryCode(string CategoryCode)
        //{
        //    return repository.GetMstInfoItemByCategoryCode(CategoryCode);
        //}

        /// <summary>
        /// 获取同步患者购买模块下的某些信息 LY 2015-10-14
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [Route("Api/v1/ModuleInfo/{UserId}/SynInfo")]
        public SynBasicInfo GetSynBasicInfoDetail(string UserId)
        {
            return repository.SynBasicInfoDetail(UserId);
        }
    }
}
