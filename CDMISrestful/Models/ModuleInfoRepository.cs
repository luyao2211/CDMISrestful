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
        public List<CmAbsType> GetHypertensionDrugNameList(string Type)
        {
            List<CmAbsType> list = new DictMethod().GetHypertensionDrug(pclsCache);
            return list.FindAll(delegate(CmAbsType x)
            {
                return x.Type.Equals(Type);
            });
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
        public List<CmAbsType> GetDiabetesDrugNameList(string Type)
        {
            List<CmAbsType> list = new DictMethod().GetDiabetesDrug(pclsCache);
            return list.FindAll(delegate(CmAbsType x)
            {
                return x.Type.Equals(Type);
            });
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
        public int SetPatBasicInfoDetail(string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            return new ModuleInfoMethod().PsBasicInfoDetailSetData(pclsCache, Patient, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, revUserId, TerminalName, TerminalIP, DeviceType);
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