using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.DataModels;

namespace CDMISrestful.Models
{
    public interface IDictRepository
    {
        List<TypeAndName> GetHypertensionDrugTypeNameList();
        List<CmAbsType> GetHypertensionDrug();
        List<TypeAndName> GetDiabetesDrugTypeNameList();
        List<CmAbsType> GetDiabetesDrug();
        List<TypeAndName> GetTypeList(string Category);
        List<MstBloodPressure> GetBloodPressure();
        List<Insurance> GetInsuranceType();
    }
}