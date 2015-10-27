using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IRiskInfoRepository
    {
        string GetDescription(int SBP);
        int SetRiskResult(string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType);

        string GetRiskResult(string UserId, string AssessmentType);
        RiskInput GetRiskInput(string UserId);
        List<PsTreatmentIndicators> GetPsTreatmentIndicators(string UserId);
        int PsTreatmentIndicatorsSetData(string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int PsParametersSetData(string Indicators, string Id, string Name, string Value, string Unit, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        List<Parameters> GetParameters(string Indicators);
    }
}