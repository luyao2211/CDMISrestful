using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using CDMISrestful.DataModels;

namespace CDMISrestful.Models
{
    public class DictRepository : IDictRepository
    {
        DataConnection pclsCache = new DataConnection();
        DictMethod dictMethod = new DictMethod();
        /// <summary>
        /// 获取高血压药物类型名称列表 LY 2015-10-14
        /// </summary>
        /// <returns></returns>
        public List<TypeAndName> GetHypertensionDrugTypeNameList()
        {
            return dictMethod.CmMstHypertensionDrugGetTypeList(pclsCache);
        }

        /// <summary>
        /// 获取高血压药物名称列表 LY 2015-10-14
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<CmAbsType> GetHypertensionDrug()
        {
            return dictMethod.GetHypertensionDrug(pclsCache);
            
        }

        /// <summary>
        /// 获取糖尿病药物类型名称列表 LY 2015-10-14
        /// </summary>
        /// <returns></returns>
        public List<TypeAndName> GetDiabetesDrugTypeNameList()
        {
            return dictMethod.CmMstDiabetesDrugGetTypeList(pclsCache);
        }

        /// <summary>
        /// 获取糖尿病药物名称列表 LY 2015-10-14
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<CmAbsType> GetDiabetesDrug()
        {
            return dictMethod.GetDiabetesDrug(pclsCache);
            
        }

        /// <summary>
        /// 获取某个分类的类别 LY 2015-10-14
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public List<TypeAndName> GetTypeList(string Category)
        {
            return dictMethod.CmMstTypeGetTypeList(pclsCache, Category);
        }

        /// <summary>
        /// 获取血压等级字典表的所有信息 LY 2015-10-13
        /// </summary>
        /// <returns></returns>
        public List<MstBloodPressure> GetBloodPressure()
        {
            return new PlanInfoMethod().GetBPGrades(pclsCache);
        }

        public List<Insurance> GetInsuranceType()
        {
            try
            {
                return dictMethod.GetInsurance(pclsCache);
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "GetInsuranceType", "WebService调用异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
                throw (ex);
            }
        }
    }
}