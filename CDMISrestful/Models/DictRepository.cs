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

        /// <summary>
        /// 获取高血压药物类型名称列表 LY 2015-10-14
        /// </summary>
        /// <returns></returns>
        public List<TypeAndName> GetHypertensionDrugTypeNameList()
        {
            return new DictMethod().CmMstHypertensionDrugGetTypeList(pclsCache);
        }

        /// <summary>
        /// 获取高血压药物名称列表 LY 2015-10-14
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<CmAbsType> GetHypertensionDrug()
        {
            return new DictMethod().GetHypertensionDrug(pclsCache);
            
        }

        /// <summary>
        /// 获取糖尿病药物类型名称列表 LY 2015-10-14
        /// </summary>
        /// <returns></returns>
        public List<TypeAndName> GetDiabetesDrugTypeNameList()
        {
            return new DictMethod().CmMstDiabetesDrugGetTypeList(pclsCache);
        }

        /// <summary>
        /// 获取糖尿病药物名称列表 LY 2015-10-14
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<CmAbsType> GetDiabetesDrug()
        {
            return new DictMethod().GetDiabetesDrug(pclsCache);
            
        }

        /// <summary>
        /// 获取某个分类的类别 LY 2015-10-14
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public List<TypeAndName> GetTypeList(string Category)
        {
            return new DictMethod().CmMstTypeGetTypeList(pclsCache, Category);
        }

        /// <summary>
        /// 获取血压等级字典表的所有信息 LY 2015-10-13
        /// </summary>
        /// <returns></returns>
        public List<MstBloodPressure> GetBloodPressure()
        {
            return new PlanInfoMethod().GetBPGrades(pclsCache);
        }
    }
}