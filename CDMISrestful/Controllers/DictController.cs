using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CDMISrestful.DataModels;
using CDMISrestful.Models;

namespace CDMISrestful.Controllers
{
    public class DictController : ApiController
    {
        static readonly IDictRepository repository = new DictRepository();

        /// <summary>
        /// 获取高血压药物类型名称列表 LY 2015-10-13
        /// </summary>
        /// <returns></returns>
        [Route("Api/v1/Dict/HypertensionDrug/TypeNames")]
        public List<TypeAndName> GetHypertensionDrugTypeNameList()
        {
            return repository.GetHypertensionDrugTypeNameList();
        }

        /// <summary>
        /// 获取高血压药物名称列表 LY 2015-10-13
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        [Route("Api/v1/Dict/HypertensionDrug")]
        public List<CmAbsType> GetHypertensionDrug()
        {
            return repository.GetHypertensionDrug();
        }

        /// <summary>
        /// 获取糖尿病药物类型名称列表 LY 2015-10-13
        /// </summary>
        /// <returns></returns>
        [Route("Api/v1/Dict/DiabetesDrug/TypeNames")]
        public List<TypeAndName> GetDiabetesDrugTypeNameList()
        {
            return repository.GetDiabetesDrugTypeNameList();
        }

        /// <summary>
        /// 获取糖尿病药物名称列表 LY 2015-10-13
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        [Route("Api/v1/Dict/DiabetesDrug")]
        public List<CmAbsType> GetDiabetesDrug()
        {
            return repository.GetDiabetesDrug();
        }

        /// <summary>
        /// 获取某个分类的类别 LY 2015-10-13
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        [Route("Api/v1/Dict/Type/{Category}")]
        public List<TypeAndName> GetTypeList(string Category)
        {
            return repository.GetTypeList(Category);
        }
    }
}
