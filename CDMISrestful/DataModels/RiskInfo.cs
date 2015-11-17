using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class RiskInfo
    {
    }
    public class M1Risk
    {       
        public double Hyper { get; set; }
        public double Harvard { get; set; }
        public double Framingham { get; set; }
        public double StrokeRisk { get; set; }
        public double HeartFailureRisk { get; set; }
        public int SBP { get; set; }
        public int DBP { get; set; }

    }
    public class RiskResult
    {
        public string UserId { get; set; }
        public int SortNo { get; set; }
        public string AssessmentType { get; set; }
        public string AssessmentName { get; set; }
        public DateTime AssessmentTime { get; set; }
        public string Result { get; set; }
        public string revUserId { get; set; }
        public string TerminalName { get; set; }
        public string TerminalIP { get; set; }
        public int DeviceType { get; set; }
    }
    public class Parameters
    {
        public string Indicators { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
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

    public class M1RiskInput
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
        public int SBP { get; set; }
        public int DBP { get; set; }
    }

    public class M3RiskInput
    {
        public int Age { get; set; }
        public int Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public double BMI { get; set; }
        public int Smoke { get; set; }
        public int Diabetes { get; set; }
        public double Creatinine { get; set; }      
        public int SBP { get; set; }

        public double EF { get; set; }
        public int NYHA { get; set; }
        public int Lung { get; set; }
        public double HF18 { get; set; }

        public int Beta { get; set; }
        public int AA { get; set; }
    }

    public class M3Risk
    {
        public double One { get; set; }
        public double Three { get; set; }

    }

}