using CDMISrestful.DataModels;
using CDMISrestful.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.CommonLibrary;

namespace CDMISrestful.Controllers
{
    public class VitalInfoController : ApiController
    {
        static readonly IVitalInfoRepository repository = new VitalInfoRepository();

        /// <summary>
        /// GetLatestPatientVitalSigns 获取病人最新体征情况 GL 2015-10-12
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ItemType"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        [Route("Api/v1/VitalInfo/VitalSign")]
        public string GetLatestPatientVitalSigns(string UserId, string ItemType, string ItemCode)
        {
            return repository.GetLatestPatientVitalSigns(UserId, ItemType, ItemCode);
        }

        /// <summary>
        /// SetPatientVitalSigns  GL 2015-10-12  
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [Route("Api/v1/VitalInfo/VitalSign")]
        [ModelValidationFilter]
        public HttpResponseMessage PostPatientVitalSigns(SetVitalInfo item)
        {
            int ret = repository.SetPatientVitalSigns(item.UserId, Convert.ToInt32(item.RecordDate), Convert.ToInt32(item.RecordTime), item.ItemType, item.ItemCode, item.Value, item.Unit, item.revUserId, item.TerminalName, item.TerminalIP, item.DeviceType);
            return new ExceptionHandler().SetData(ret);
        }

        /// <summary>
        /// GetSignsDetailByPeriod 获取某日期之前，一定条数血压（收缩压/舒张压）和脉率的数据详细时刻列表,用于phone，支持继续加载  GL 2015-10-12  
        /// </summary>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <param name="StartDate"></param>
        /// <param name="Num"></param>
        [Route("Api/v1/VitalInfo/VitalSigns")]
        [HttpGet]
        public SignDetailByP GetSignsDetailByPeriod(string PatientId, string Module, int StartDate, int Num)
        {
            return repository.GetSignsDetailByPeriod(PatientId, Module, StartDate, Num);
        }
    }
}
