using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public class RiskInfoRepository : IRiskInfoRepository
    {
        DataConnection pclsCache = new DataConnection();
        /// <summary>
        /// 获取某计划下某任务的目标值 LY 2015-10-13
        /// </summary>
        /// <param name="PlanNo"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetValueByPlanNoAndId(string PlanNo, string Id)
        {
            return new PlanInfoMethod().GetValueByPlanNoAndId(pclsCache, PlanNo, Id);
        }

       

        /// <summary>
        /// 根据收缩压获取血压等级说明 LY 2015-10-13
        /// </summary>
        /// <param name="SBP"></param>
        /// <returns></returns>
        public string GetDescription(int SBP)
        {
            return new PlanInfoMethod().GetDescription(pclsCache, SBP);
        }

        /// <summary>
        /// 插入风险评估结果 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="AssessmentType"></param>
        /// <param name="AssessmentName"></param>
        /// <param name="AssessmentTime"></param>
        /// <param name="Result"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int SetRiskResult(string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int SortNo = new RiskInfoMethod().GetMaxSortNo(pclsCache, UserId) + 1;    //SortNo自增
            return new RiskInfoMethod().PsTreatmentIndicatorsSetData(pclsCache, UserId, SortNo, AssessmentType, AssessmentName, AssessmentTime, Result, revUserId, TerminalName, TerminalIP, DeviceType);
        }

        public int PsTreatmentIndicatorsSetData(string UserId, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int SortNo = new RiskInfoMethod().GetMaxSortNo(pclsCache, UserId) + 1;    //SortNo自增
            return new RiskInfoMethod().PsTreatmentIndicatorsSetData(pclsCache, UserId, SortNo, AssessmentType, AssessmentName, AssessmentTime, Result, revUserId, TerminalName, TerminalIP, DeviceType);
        }

        public int PsParametersSetData(string Indicators, string Id, string Name, string Value, string Unit, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            return new RiskInfoMethod().PsParametersSetData(pclsCache, Indicators, Id, Name, Value, Unit, revUserId, TerminalName, TerminalIP, DeviceType);
        }
        /// <summary>
        /// 根据UserId获取最新风险评估结果 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetRiskResult(string UserId, string AssessmentType)
        {
            int SortNo = new RiskInfoMethod().GetMaxSortNo(pclsCache, UserId);
            return new RiskInfoMethod().GetResult(pclsCache, UserId, SortNo, AssessmentType);
        }

        /// <summary>
        /// 获取风险评估所需输入 LY 2015-10-13
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RiskInput GetRiskInput(string UserId)
        {
            //当用户缺少某项参数时，设置一个默认值
            int Age = 1;//年龄默认1岁（避免出现0岁）
            int Gender = 0;//性别男
            int Height = 0;//身高176cm
            int Weight = 0;//体重69千克
            int AbdominalGirth = 0; //腹围
            int Heartrate = 0;//心率
            int Parent = 0;//父母中至少有一方有高血压
            int Smoke = 0;//不抽烟
            int Stroke = 0;//没有中风
            int Lvh = 0; ;//有左心室肥大
            int Diabetes = 0;//有伴随糖尿病
            int Treat = 0;//高血压是否在治疗（接受过）没有
            int Heartattack = 0;//有过心脏事件（心血管疾病）
            int Af = 0;//没有过房颤
            int Chd = 0;//有冠心病(心肌梗塞)
            int Valve = 0;//没有心脏瓣膜病
            double Tcho = 0;//总胆固醇浓度5.2mmol/L
            double Creatinine = 0;//肌酐浓度140μmoI/L
            double Hdlc = 0;//高密度脂蛋白胆固醇1.21g/ml
            int SBP = 0;//当前收缩压
            int DBP = 0;//当前舒张压
            //用于取得真实值
            int piParent = 0;//父母中至少有一方有高血压
            int piSmoke = 0;//不抽烟
            int piStroke = 0;//没有中风
            int piLvh = 0; ;//有左心室肥大
            int piDiabetes = 0;//有伴随糖尿病
            int piTreat = 0;//高血压是否在治疗（接受过）没有
            int piHeartattack = 0;//有过心脏事件（心血管疾病）
            int piAf = 0;//没有过房颤
            int piChd = 0;//有冠心病(心肌梗塞)
            int piValve = 0;//没有心脏瓣膜病
            BasicInfo BaseList = new UsersMethod().GetBasicInfo(pclsCache, UserId);
            if (BaseList != null)
            {
                if (BaseList.Birthday != "" && BaseList.Birthday != "0" && BaseList.Birthday != null)
                {
                    Age = new UsersMethod().GetAgeByBirthDay(pclsCache, Convert.ToInt32(BaseList.Birthday));//年龄
                }
                if (BaseList.Gender != "" && BaseList.Gender != "0" && BaseList.Gender != null)
                {
                    Gender = Convert.ToInt32(BaseList.Gender);//性别
                }
            }
            if (Gender <= 2)
            {
                Gender = Gender - 1;
            }
            else
            {
                Gender = 0;
            }
            if (Gender == 1)//为计算方便，性别值对调
            {
                Gender = 0;
            }
            else
            {
                Gender = 1;
            }
            //获取体重，身高和BMI
            string Weight1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_02", 1);
            if (Weight1 != "" && Weight1 != "0" && Weight1 != null)
            {
                Weight = Convert.ToInt32(Weight1);
            }
            string Height1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_01", 1);
            if (Height1 != "" && Height1 != "0" && Height1 != null)
            {
                Height = Convert.ToInt32(Height1);
            }
            string BMIStr = ((double)Weight / ((double)Height * (double)Height) * 10000).ToString("f2");
            double BMI = double.Parse(BMIStr);
            //获取腹围
            string AbdominalGirth1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_13", 1);
            if (AbdominalGirth1 != "" && AbdominalGirth1 != "0" && AbdominalGirth1 != null)
            {
                AbdominalGirth = Convert.ToInt32(AbdominalGirth1);
            }
            //获取心率
            string Heart = new VitalInfoMethod().GetLatestPatientVitalSigns(pclsCache, UserId, "HeartRate", "HeartRate_1");
            if (Heart != "" && Heart != "0" && Heart != null)
            {
                Heartrate = Convert.ToInt32(Heart);
            }
            //获取遗传信息，即父母有无高血压，1是2否3未知
            string Parent1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1002_01", 1);
            if (Parent1 != "" && Parent1 != "0" && Parent1 != null)
            {
                Parent = Convert.ToInt32(Parent1);
                piParent = Parent;
            }
            if (Parent > 1)
            {
                Parent = 0;
            }
            //获取是否抽烟1是2否3未知
            string Smoke1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1005_04", 1);
            if (Smoke1 != "" && Smoke1 != "0" && Smoke1 != null)
            {
                Smoke = Convert.ToInt32(Smoke1);
                piSmoke = Smoke;
            }
            if (Smoke > 1)
            {
                Smoke = 0;
            }
            //获取是否中风
            string Stroke1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_07", 1);
            if (Stroke1 != "" && Stroke1 != "0" && Stroke1 != null)
            {
                Stroke = Convert.ToInt32(Stroke1);
                piStroke = Stroke;
            }
            if (Stroke > 1)
            {
                Stroke = 0;
            }
            //获取是否左心室肥大
            string Lvh1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_09", 1);
            if (Lvh1 != "" && Lvh1 != "0" && Lvh1 != null)
            {
                Lvh = Convert.ToInt32(Lvh1);
                piLvh = Lvh;
            }
            if (Lvh > 1)
            {
                Lvh = 0;
            }
            //获取是否糖尿病
            string Diabetes1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1002_02", 1);
            if (Diabetes1 != "" && Diabetes1 != "0" && Diabetes1 != null)
            {
                Diabetes = Convert.ToInt32(Diabetes1);
                piDiabetes = Diabetes;
            }
            if (Diabetes > 1)
            {
                Diabetes = 0;
            }
            //高血压是否在治疗（是否接受高血压治疗）
            string Treat1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1003_02", 1);
            if (Treat1 != "" && Treat1 != "0" && Treat1 != null)
            {
                Treat = Convert.ToInt32(Treat1);
                piTreat = Treat;
            }
            if (Treat > 1)
            {
                Treat = 0;
            }
            //是否有心脏事件（心血管疾病,心脏骤停）
            string Heartattack1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_04", 1);
            if (Heartattack1 != "" && Heartattack1 != "0" && Heartattack1 != null)
            {
                Heartattack = Convert.ToInt32(Heartattack1);
                piHeartattack = Heartattack;
            }
            if (Heartattack > 1)
            {
                Heartattack = 0;
            }
            //是否有房颤
            string Af1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_05", 1);
            if (Af1 != "" && Af1 != "0" && Af1 != null)
            {
                Af = Convert.ToInt32(Af1);
                piAf = Af;
            }
            if (Af > 1)
            {
                Af = 0;
            }
            //是否有冠心病（心肌梗塞）
            string Chd1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_02", 1);
            if (Chd1 != "" && Chd1 != "0" && Chd1 != null)
            {
                Chd = Convert.ToInt32(Chd1);
                piChd = Chd;
            }
            if (Chd > 1)
            {
                Chd = 0;
            }
            //是否有心脏瓣膜病
            string Valve1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1001_06", 1);
            if (Valve1 != "" && Valve1 != "0" && Valve1 != null)
            {
                Valve = Convert.ToInt32(Valve1);
                piValve = Valve;
            }
            if (Valve > 1)
            {
                Valve = 0;
            }
            //总胆固醇浓度
            string Tcho1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_09", 1);
            if (Tcho1 != "" && Tcho1 != "0" && Tcho1 != null)
            {
                Tcho = Convert.ToDouble(Tcho1);
            }
            //肌酐浓度
            string Creatinine1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_08", 1);
            if (Creatinine1 != "" && Creatinine1 != "0" && Creatinine1 != null)
            {
                Creatinine = Convert.ToDouble(Creatinine1);
            }
            //高密度脂蛋白胆固醇
            string Hdlc1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_10", 1);
            if (Hdlc1 != "" && Hdlc1 != "0" && Hdlc1 != null)
            {
                Hdlc = Convert.ToDouble(Hdlc1);
            }
            //收缩压和舒张压
            string SBP1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_05", 1);
            if (SBP1 != "" && SBP1 != "0" && SBP1 != null)
            {
                SBP = Convert.ToInt32(SBP1);
            }
            string DBP1 = new ModuleInfoMethod().PsBasicInfoDetailGetValue(pclsCache, UserId, "M1", "M1006_06", 1);
            if (DBP1 != "" && DBP1 != "0" && DBP1 != null)
            {
                DBP = Convert.ToInt32(DBP1);
            }
            //高血压风险，除血压外的风险已经计算好放在Hyperother中，界面上取了血压之后，加上血压的风险即可。
            double Hyperother = -0.15641 * Age - 0.20293 * Gender - 0.19073 * Smoke - 0.16612 * Parent - 0.03388 * BMI;
            //HarvardRiskInfactor这个变量存的是Harvard风险评估计算公式中的风险因数，界面上需要做的是加上收缩压的风险因数，然后代入公式计算。
            int HarvardRiskInfactor = 0;
            if (Gender == 1)
            {
                if (Age <= 39)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 19;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 0;
                    }
                }
                else if (Age <= 44 && Age >= 40)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 7;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 4;
                    }
                }
                else if (Age <= 49 && Age >= 45)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 7;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 7;
                    }
                }
                else if (Age <= 54 && Age >= 50)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 11;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 6;
                    }
                }
                else if (Age <= 59 && Age >= 55)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 14;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 6;
                    }
                }
                else if (Age <= 64 && Age >= 60)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 18;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 5;
                    }
                }
                else if (Age <= 69 && Age >= 65)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 22;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 4;
                    }
                }
                else
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 25;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 4;
                    }
                }
                //年龄和抽烟的风险值加成
                if (Tcho < 5)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                else if (Tcho >= 5.0 && Tcho <= 5.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else if (Tcho >= 6.0 && Tcho <= 6.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 4;
                }
                else if (Tcho >= 7.0 && Tcho <= 7.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 5;
                }
                else if (Tcho >= 8.0 && Tcho <= 8.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 7;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 9;
                }
                //总胆固醇浓度风险值加成
                if (Height < 145)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 6;
                }
                else if (Height >= 145 && Height <= 154)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 4;
                }
                else if (Height >= 155 && Height <= 164)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                else if (Height >= 165 && Height <= 174)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                //身高风险值加成
                if (Creatinine < 50)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                else if (Creatinine >= 50 && Creatinine <= 69)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 1;
                }
                else if (Creatinine >= 70 && Creatinine <= 89)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else if (Creatinine >= 90 && Creatinine <= 109)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 4;
                }
                //肌酐浓度风险值加成
                if (Chd == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 8;
                }
                //心肌梗塞（冠心病）风险值加成
                if (Stroke == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 8;
                }
                //中风风险值加成 
                if (Lvh == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                //左室高血压（左心室肥大）风险值加成
                if (Diabetes == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                //糖尿病风险值加成
            }
            //以上是男性风险值
            else
            {
                if (Age <= 39)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 13;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 0;
                    }
                }
                else if (Age <= 44 && Age >= 40)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 12;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 5;
                    }
                }
                else if (Age <= 49 && Age >= 45)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 11;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 9;
                    }
                }
                else if (Age <= 54 && Age >= 50)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 10;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 14;
                    }
                }
                else if (Age <= 59 && Age >= 55)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 10;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 18;
                    }
                }
                else if (Age <= 64 && Age >= 60)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 9;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 23;
                    }
                }
                else if (Age <= 69 && Age >= 65)
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 9;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 27;
                    }
                }
                else
                {
                    if (Smoke == 1)
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 8;
                    }
                    else
                    {
                        HarvardRiskInfactor = HarvardRiskInfactor + 32;
                    }
                }
                //年龄和抽烟的风险值加成
                if (Tcho < 5)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                else if (Tcho >= 5.0 && Tcho <= 5.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                else if (Tcho >= 6.0 && Tcho <= 6.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 1;
                }
                else if (Tcho >= 7.0 && Tcho <= 7.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 1;
                }
                else if (Tcho >= 8.0 && Tcho <= 8.9)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                //总胆固醇浓度风险值加成
                if (Height < 145)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 6;
                }
                else if (Height >= 145 && Height <= 154)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 4;
                }
                else if (Height >= 155 && Height <= 164)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                else if (Height >= 165 && Height <= 174)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                //身高风险值加成
                if (Creatinine < 50)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 0;
                }
                else if (Creatinine >= 50 && Creatinine <= 69)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 1;
                }
                else if (Creatinine >= 70 && Creatinine <= 89)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 2;
                }
                else if (Creatinine >= 90 && Creatinine <= 109)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                else
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 4;
                }
                //肌酐浓度风险值加成
                if (Chd == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 8;
                }
                //心肌梗塞（冠心病）风险值加成
                if (Stroke == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 8;
                }
                //中风风险值加成 
                if (Lvh == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 3;
                }
                //左室高血压（左心室肥大）风险值加成
                if (Diabetes == 1)
                {
                    HarvardRiskInfactor = HarvardRiskInfactor + 9;
                }
                //糖尿病风险值加成
            }
            //以上是女性风险值
            //FraminghamRiskInfactor这个变量存的是Framingham风险评估计算公式中的风险因数，界面上需要做的是加上收缩压的风险因数，然后代入公式计算。
            //这个Framingham模型也是需要收缩压值的，分为接受过治疗的血压和未接受过治疗的血压，模型分为男女进行计算，因为不同性别公式不同
            double FraminghamRiskInfactor = 0.0;
            if (Gender == 1) //男性
            {
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Age) * 3.06117;//性别
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Tcho) * 1.12370;//总胆固醇
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Hdlc) * (-0.93263);//高密度脂蛋白胆固醇
                if (Smoke == 1)
                {
                    FraminghamRiskInfactor = FraminghamRiskInfactor + 0.65451;//抽烟
                }
                if (Diabetes == 1)
                {
                    FraminghamRiskInfactor = FraminghamRiskInfactor + 0.57367;//抽烟
                }
            }
            else //女性
            {
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Age) * 2.3288;//性别
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Tcho) * 1.20904;//总胆固醇
                FraminghamRiskInfactor = FraminghamRiskInfactor + Math.Log(Hdlc) * (-0.70833);//高密度脂蛋白胆固醇
                if (Smoke == 1)
                {
                    FraminghamRiskInfactor = FraminghamRiskInfactor + 0.52873;//抽烟
                }
                if (Diabetes == 1)
                {
                    FraminghamRiskInfactor = FraminghamRiskInfactor + 0.69154;//抽烟
                }
            }
            //StrokeRiskInfactor这个变量存的是中风风险评估计算公式中的风险因数，界面上需要做的是加上收缩压的风险因数，然后计算。
            int StrokeRiskInfactor = 0;
            if (Gender == 1) //男性
            {
                if (Age <= 56)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 0;
                }
                else if (Age >= 57 && Age <= 59)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 1;
                }
                else if (Age >= 60 && Age <= 62)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 2;
                }
                else if (Age >= 63 && Age <= 65)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 3;
                }
                else if (Age >= 66 && Age <= 68)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 4;
                }
                else if (Age >= 69 && Age <= 72)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 5;
                }
                else if (Age >= 73 && Age <= 75)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 6;
                }
                else if (Age >= 76 && Age <= 78)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 7;
                }
                else if (Age >= 79 && Age <= 81)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 8;
                }
                else if (Age >= 82 && Age <= 84)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 9;
                }
                else
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 10;
                }
                if (Diabetes == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 2;
                }
                //糖尿病风险值加成
                if (Smoke == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 3;
                }
                //吸烟风险值加成
                if (Heartattack == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 4;
                }
                //心血管疾病史（心脏事件）风险值加成
                if (Af == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 4;
                }
                //房颤风险值加成
                if (Lvh == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 5;
                }
            }
            else //女性
            {
                if (Age <= 56)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 0;
                }
                else if (Age >= 57 && Age <= 59)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 1;
                }
                else if (Age >= 60 && Age <= 62)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 2;
                }
                else if (Age >= 63 && Age <= 64)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 3;
                }
                else if (Age >= 65 && Age <= 67)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 4;
                }
                else if (Age >= 68 && Age <= 70)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 5;
                }
                else if (Age >= 71 && Age <= 73)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 6;
                }
                else if (Age >= 74 && Age <= 76)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 7;
                }
                else if (Age >= 77 && Age <= 78)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 8;
                }
                else if (Age >= 79 && Age <= 81)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 9;
                }
                else
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 10;
                }
                if (Diabetes == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 3;
                }
                //糖尿病风险值加成
                if (Smoke == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 3;
                }
                //吸烟风险值加成
                if (Heartattack == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 2;
                }
                //心血管疾病史（心脏事件）风险值加成
                if (Af == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 6;
                }
                //房颤风险值加成
                if (Lvh == 1)
                {
                    StrokeRiskInfactor = StrokeRiskInfactor + 4;
                }
            }
            //HeartFailureRiskInfactor这个变量存的是心衰风险评估计算公式中的风险因数，界面上需要做的是加上收缩压的风险因数，然后计算。
            int HeartFailureRiskInfactor = 0;
            if (Gender == 1) //男性
            {
                if (Age <= 49)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 0;
                }
                else if (Age >= 50 && Age <= 54)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                else if (Age >= 55 && Age <= 59)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                }
                else if (Age >= 60 && Age <= 64)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 3;
                }
                else if (Age >= 65 && Age <= 69)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 4;
                }
                else if (Age >= 70 && Age <= 74)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 5;
                }
                else if (Age >= 75 && Age <= 79)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 6;
                }
                else if (Age >= 80 && Age <= 84)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 7;
                }
                else if (Age >= 85 && Age <= 89)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 8;
                }
                else
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 9;
                }
                if (Heartrate <= 54)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 0;
                }
                else if (Heartrate >= 55 && Heartrate <= 64)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                else if (Heartrate >= 65 && Heartrate <= 79)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                }
                else if (Heartrate >= 80 && Heartrate <= 89)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 3;
                }
                else if (Heartrate >= 90 && Heartrate <= 104)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 4;
                }
                else
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 5;
                }
                //心率风险值加成
                if (Lvh == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 4;
                }
                //左心室肥大（左室高血压）风险值加成
                if (Chd == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 8;
                }
                //冠心病风险值加成
                if (Valve == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 5;
                }
                //瓣膜疾病风险值加成
                if (Smoke == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                //糖尿病风险值加成
            }
            else //女性
            {
                if (Age <= 49)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 0;
                }
                else if (Age >= 50 && Age <= 54)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                else if (Age >= 55 && Age <= 59)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                }
                else if (Age >= 60 && Age <= 64)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 3;
                }
                else if (Age >= 65 && Age <= 69)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 4;
                }
                else if (Age >= 70 && Age <= 74)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 5;
                }
                else if (Age >= 75 && Age <= 79)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 6;
                }
                else if (Age >= 80 && Age <= 84)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 7;
                }
                else if (Age >= 85 && Age <= 89)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 8;
                }
                else
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 9;
                }
                //年龄的风险加权值
                if (Heartrate < 60)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 0;
                }
                else if (Heartrate >= 60 && Heartrate <= 79)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                else if (Heartrate >= 80 && Heartrate <= 104)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                }
                else
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 3;
                }
                //心率风险值加成
                if (Lvh == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 5;
                }
                //左心室肥大（左室高血压）风险值加成
                if (Chd == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 6;
                }
                //冠心病风险值加成
                if (Valve == 1)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 6;
                    if (Smoke == 1)
                    {
                        HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                    }
                }
                else
                {
                    if (Smoke == 1)
                    {
                        HeartFailureRiskInfactor = HeartFailureRiskInfactor + 6;
                    }
                }
                //瓣膜疾病和糖尿病风险值加成
                if (BMI < 21)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 0;
                }
                else if (BMI >= 21 && BMI <= 25)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 1;
                }
                else if (BMI > 25 && BMI <= 29)
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 2;
                }
                else
                {
                    HeartFailureRiskInfactor = HeartFailureRiskInfactor + 3;
                }
            }
            //BMI风险值加成
            RiskInput Input = new RiskInput();
            Input.Age = Age;
            Input.Gender = Gender;
            Input.Height = Height;
            Input.Weight = Weight;
            Input.AbdominalGirth = AbdominalGirth;
            Input.BMI = BMI;
            Input.Heartrate = Heartrate;
            Input.Parent = Parent;
            Input.Smoke = Smoke;
            Input.Stroke = Stroke;
            Input.Lvh = Lvh;
            Input.Diabetes = Diabetes;
            Input.Treat = Treat;
            Input.Heartattack = Heartattack;
            Input.Af = Af;
            Input.Chd = Chd;
            Input.Valve = Valve;
            Input.Tcho = Tcho;
            Input.Creatinine = Creatinine;
            Input.Hdlc = Hdlc;
            Input.Hyperother = Hyperother;
            Input.HarvardRiskInfactor = HarvardRiskInfactor;
            Input.FraminghamRiskInfactor = FraminghamRiskInfactor;
            Input.StrokeRiskInfactor = StrokeRiskInfactor;
            Input.HeartFailureRiskInfactor = HeartFailureRiskInfactor;
            Input.SBP = SBP;
            Input.DBP = DBP;
            Input.piParent = piParent;
            Input.piSmoke = piSmoke;
            Input.piStroke = piStroke;
            Input.piLvh = piLvh;
            Input.piDiabetes = piDiabetes;
            Input.piTreat = piTreat;
            Input.piHeartattack = piHeartattack;
            Input.piAf = piAf;
            Input.piChd = piChd;
            Input.piValve = piValve;
            return Input;
        }
        public List<PsTreatmentIndicators> GetPsTreatmentIndicators(string UserId)
        {
            return new RiskInfoMethod().GetPsTreatmentIndicators(pclsCache, UserId);

        }
        public List<Parameters> GetParameters(string Indicators)
        {
            return new RiskInfoMethod().GetParameters(pclsCache, Indicators);

        }
    }
}