using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public class ModuleInfoRepository : IModuleInfoRepository
    {
        DataConnection pclsCache = new DataConnection();

        /// <summary>
        /// 输入PatientId和CategoryCode，获取患者已经购买的某个模块的详细信息 LY 2015-10-14
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<PatBasicInfoDetail> GetItemInfoByPIdAndModule(string UserId, string CategoryCode)
        {
            return new ModuleInfoMethod().PsBasicInfoDetailGetPatientBasicInfoDetail(pclsCache, UserId, CategoryCode);
        }

     

        /// <summary>
        /// 获取模块关注详细信息 LY 2015-10-14
        /// </summary>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<MstInfoItemByCategoryCode> GetMstInfoItemByCategoryCode(string CategoryCode)
        {
            return new DictMethod().GetMstInfoItemByCategoryCode(pclsCache, CategoryCode);
        }

        /// <summary>
        /// 同步患者购买模块下的某些信息 LY 2015-10-14
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public SynBasicInfo SynBasicInfoDetail(string UserId)
        {
            SynBasicInfo ret = new SynBasicInfo();
            ret.ExamInfo = new ClinicInfoMethod().GetNewExam(pclsCache, UserId);
            ret.LabTestInfo = new ClinicInfoMethod().GetNewLabTest(pclsCache, UserId);
            return ret;
        }
    }
}