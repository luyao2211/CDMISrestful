using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class RiskInfo
    {
    }
    public class RiskInput
    {
        public int Age { get; set; }
        public int Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int AbdominalGirth { get; set; }
        public double BMI { get; set; }
        public int Heartrate { get; set; }
        public int Parent { get; set; }
        public int Smoke { get; set; }
        public int Stroke { get; set; }
        public int Lvh { get; set; }
        public int Diabetes { get; set; }
        public int Treat { get; set; }
        public int Heartattack { get; set; }
        public int Af { get; set; }
        public int Chd { get; set; }
        public int Valve { get; set; }
        public double Tcho { get; set; }
        public double Creatinine { get; set; }
        public double Hdlc { get; set; }
        public double Hyperother { get; set; }
        public int HarvardRiskInfactor { get; set; }
        public double FraminghamRiskInfactor { get; set; }
        public int StrokeRiskInfactor { get; set; }
        public int HeartFailureRiskInfactor { get; set; }
        public int SBP { get; set; }
        public int DBP { get; set; }
        public int piParent { get; set; }
        public int piSmoke { get; set; }
        public int piStroke { get; set; }
        public int piLvh { get; set; }
        public int piDiabetes { get; set; }
        public int piTreat { get; set; }
        public int piHeartattack { get; set; }
        public int piAf { get; set; }
        public int piChd { get; set; }
        public int piValve { get; set; }
    }
    public class RiskResult
    {
        public string UserId { get; set; }
        public string AssessmentType { get; set; }
        public string AssessmentName { get; set; }
        public DateTime AssessmentTime { get; set; }
        public string Result { get; set; }
        public string revUserId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalIP { get; set; }
        public int DeviceType { get; set; }
    }
    public class PsTreatmentIndicators
    {
        public int SortNo { get; set; }
        public string AssessmentType { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentTime { get; set; }
        public string Result { get; set; }
        public string DocName { get; set; }
       
    }
}