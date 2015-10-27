using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataMethod
{
    public class RiskInfoMethod
    {
        #region < "Ps.TreatmentIndicators" >

        /// <summary>
        /// Ps.TreatmentIndicators.SetData GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SortNo"></param>
        /// <param name="AssessmentType"></param>
        /// <param name="AssessmentName"></param>
        /// <param name="AssessmentTime"></param>
        /// <param name="Result"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int PsTreatmentIndicatorsSetData(DataConnection pclsCache, string UserId, int SortNo, string AssessmentType, string AssessmentName, DateTime AssessmentTime, string Result, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.TreatmentIndicators.SetData(pclsCache.CacheConnectionObject, UserId, SortNo, AssessmentType, AssessmentName, AssessmentTime, Result, revUserId, TerminalName, TerminalIP, DeviceType);               
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.SetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return 2;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        public int PsParametersSetData(DataConnection pclsCache, string Indicators, string Id, string Name, string Value, string Unit,  string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                ret = (int)Ps.Parameters.SetData(pclsCache.CacheConnectionObject, Indicators, Id, Name, Value, Unit, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.PsParametersSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return 2;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        /// <summary>
        /// GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int GetMaxSortNo(DataConnection pclsCache, string UserId)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;

                }
                ret = (int)Ps.TreatmentIndicators.GetMaxSortNo(pclsCache.CacheConnectionObject, UserId);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetMaxSortNo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// GL 2015-10-10
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SortNo"></param>
        /// <returns></returns>
        public string GetResult(DataConnection pclsCache, string UserId, int SortNo, string AssessmentType)
        {
            string Result = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return "";

                }
                Result = Ps.TreatmentIndicators.GetResult(pclsCache.CacheConnectionObject, UserId, SortNo, AssessmentType);
                return Result;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetResult", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return Result;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        public List<PsTreatmentIndicators> GetPsTreatmentIndicators(DataConnection pclsCache, string UserId)
        {
            List<PsTreatmentIndicators> list = new List<PsTreatmentIndicators>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.TreatmentIndicators.GetPsTreatmentIndicators(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserId", CacheDbType.NVarChar).Value = UserId;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new PsTreatmentIndicators
                    {
                        SortNo = Convert.ToInt32(cdr["SortNo"]),
                        AssessmentType = cdr["AssessmentType"].ToString(),
                        AssessmentName = cdr["AssessmentName"].ToString(),
                        AssessmentTime = Convert.ToDateTime(cdr["AssessmentTime"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        Result = cdr["Result"].ToString(),
                        DocName = cdr["DocName"].ToString(),
                       
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetPsTreatmentIndicators", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }
        public List<Parameters> GetParameters(DataConnection pclsCache, string Indicators)
        {
            List<Parameters> list = new List<Parameters>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Parameters.GetParameters(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("Indicators", CacheDbType.NVarChar).Value = Indicators;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new Parameters
                    {
                        Id = cdr["Id"].ToString(),
                        Name = cdr["Name"].ToString(),
                        Value = cdr["Value"].ToString(),
                        Unit = cdr["Unit"].ToString()
                       
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "RiskInfoMethod.GetParameters", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                if ((cdr != null))
                {
                    cdr.Close();
                    cdr.Dispose(true);
                    cdr = null;
                }
                if ((cmd != null))
                {
                    cmd.Parameters.Clear();
                    cmd.Dispose();
                    cmd = null;
                }
                pclsCache.DisConnect();
            }
        }
        
        #endregion
    }
}