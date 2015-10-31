using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.Models
{
    public interface IVitalInfoRepository
    {
        /// <summary>
        /// 获取最新一条体征信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ItemType"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        string GetLatestPatientVitalSigns(string UserId, string ItemType, string ItemCode);
        int SetPatientVitalSigns(string UserId, int RecordDate, int RecordTime, string ItemType, string ItemCode, string Value, string Unit, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        SignDetailByP GetSignsDetailByPeriod(string PatientId, string Module, int StartDate, int Num);
        //List<VitalInfo> GetAllSignsByPeriod( string UserId, int StartDate, int EndDate);

    }
}