using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IRiskInfoRepository
    {
        string GetDescription(DataConnection pclsCache, int SBP);
        int SetRiskResult(DataConnection pclsCache, string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType);

        string GetRiskResult(DataConnection pclsCache, string UserId, string AssessmentType);
        RiskInput GetRiskInput(DataConnection pclsCache, string UserId);
        List<PsTreatmentIndicators> GetPsTreatmentIndicators(DataConnection pclsCache, string UserId);
        int PsTreatmentIndicatorsSetData(DataConnection pclsCache, string UserId, int SortNo, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int PsParametersSetData(DataConnection pclsCache, string Indicators, string Id, string Name, string Value, string Unit, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        List<Parameters> GetParameters(DataConnection pclsCache, string Indicators);
        int GetMaxSortNo(DataConnection pclsCache, string UserId);
    }
}