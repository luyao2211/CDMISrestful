using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMISrestful.CommonLibrary;
using InterSystems.Data.CacheClient;
using CDMISrestful.DataModels;

namespace CDMISrestful.DataMethod
{
    public class UsersMethod
    {   /// <summary>
        ///  GetUserInfoByUserId ZAM 2014-12-02 //syf 20151014
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserInfoByUserId GetUserInfoByUserId(DataConnection pclsCache, string UserId)
        {
            UserInfoByUserId ret = new UserInfoByUserId();
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Cm.MstUser.GetUserInfoByUserId(pclsCache.CacheConnectionObject, UserId);
                if (list != null)
                {
                    ret.UserId = list[0];
                    ret.UserName = list[1];
                    ret.Password = list[2];
                    ret.Class = list[3];
                    ret.ClassName = list[4];
                    ret.StartDate = list[5];
                    ret.EndDate = list[6];
                }
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstUser.GetUserInfoByUserId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        public bool CheckUserExist(DataConnection pclsCache, string UserId)
        {
            bool exist = false;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return exist;
                }

                int flag = (int)Cm.MstUser.CheckExist(pclsCache.CacheConnectionObject, UserId);
                if (flag == 1)
                {
                    exist = true;
                }
                return exist;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.CheckUserExist", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return exist;
            }
            finally
            {
                pclsCache.DisConnect();
            }


        }
        /// <summary>
        /// 根据手机号获取userId LS 2015-03-26  TDY 20150507修改 //syf 20151013
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string GetIDByInputPhone(DataConnection pclsCache, string Type, string Name)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = Cm.MstUserDetail.GetIDByInput(pclsCache.CacheConnectionObject, Type, Name);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetIDByInputPhone", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public int CheckPasswordByInput(DataConnection pclsCache, string Type, string Name, string Password)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUserDetail.CheckPasswordByInput(pclsCache.CacheConnectionObject, Type, Name, Password);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.CheckPasswordByInput", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        #region<CmMstUserDetail>
        /// <summary>
        /// 王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string GetIDByInput(DataConnection pclsCache, string Type, string Name)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = Cm.MstUserDetail.GetIDByInput(pclsCache.CacheConnectionObject, Type, Name);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetIDByInput", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        ///  CheckRepeat LS 2015-03-26  TDY 20150507修改 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Input"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int CheckRepeat(DataConnection pclsCache, string Input, string Type)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUserDetail.CheckRepeat(pclsCache.CacheConnectionObject, Input, Type);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.CheckRepeat", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        #endregion

        #region<CmMstUser>
        /// <summary>
        /// ChangePassword ZAM 2014-12-01 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="OldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int ChangePassword(DataConnection pclsCache, string UserId, string OldPassword, string newPassword, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return ret;

                }
                ret = (int)Cm.MstUser.ChangePassword(pclsCache.CacheConnectionObject, UserId, OldPassword, newPassword, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.ChangePassword", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }


        }
        /// <summary>
        /// Register TDY 2015-04-07 专员注册 //TDY 20150419修改 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Type"></param>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <param name="Password"></param>
        /// <param name="UserName"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int Register(DataConnection pclsCache, string Type, string Name, string Value, string Password, string UserName, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Cm.MstUser.Register(pclsCache.CacheConnectionObject, Type, Name, Value, Password, UserName, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.Register", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        /// <summary>
        ///  GetNameByUserId ZAM 2014-11-26 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetNameByUserId(DataConnection pclsCache, string UserId)
        {
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }
                string Name = Cm.MstUser.GetNameByUserId(pclsCache.CacheConnectionObject, UserId);
                return Name;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetNameByUserId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        #endregion

        #region<PsRoleMatch>
        /// <summary>
        /// 王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserID"></param>
        /// <param name="RoleClass"></param>
        /// <returns></returns>
        public string GetActivatedState(DataConnection pclsCache, string UserID, string RoleClass)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = Ps.RoleMatch.GetActivatedState(pclsCache.CacheConnectionObject, UserID, RoleClass);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetActivatedState", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<string> GetAllRoleMatch(DataConnection pclsCache, string UserID)
        {
            List<string> list = new List<string>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.RoleMatch.GetAllRoleMatch(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("UserID", CacheDbType.NVarChar).Value = UserID;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(cdr["RoleClass"].ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetAllRoleMatch", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// TDY 2015-05-26 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="PatientId"></param>
        /// <param name="RoleClass"></param>
        /// <param name="ActivationCode"></param>
        /// <param name="ActivatedState"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public int PsRoleMatchSetData(DataConnection pclsCache, string PatientId, string RoleClass, string ActivationCode, string ActivatedState, string Description)
        {
            int ret = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Ps.RoleMatch.SetData(pclsCache.CacheConnectionObject, PatientId, RoleClass, ActivationCode, ActivatedState, Description);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.PsRoleMatchSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        /// <summary>
        ///  TDY 20150526 SetActivition //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserID"></param>
        /// <param name="RoleClass"></param>
        /// <param name="ActivationCode"></param>
        /// <returns></returns>
        public int SetActivition(DataConnection pclsCache, string UserID, string RoleClass, string ActivationCode)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Ps.RoleMatch.SetActivition(pclsCache.CacheConnectionObject, UserID, RoleClass, ActivationCode);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.SetActivition", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 根据角色获取激活用户 2015-06-03 ZC //SYF 20151022
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="RoleClass"></param>
        /// <returns></returns>
        public List<ActiveUser> GetActiveUserByRole(DataConnection pclsCache, string RoleClass)
        {
            List<ActiveUser> list = new List<ActiveUser>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.RoleMatch.GetActiveUserByRole(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("RoleClass", CacheDbType.NVarChar).Value = RoleClass;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    ActiveUser NewLine = new ActiveUser();
                    NewLine.UserName = cdr["UserName"].ToString();
                    NewLine.UserId = cdr["UserId"].ToString();
                    list.Add(NewLine);
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetInactiveUserByRole", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        #region<PsBasicInfo>
        /// <summary>
        /// SetData WF 2014-12-2 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="Birthday"></param>
        /// <param name="Gender"></param>
        /// <param name="BloodType"></param>
        /// <param name="IDNo"></param>
        /// <param name="DoctorId"></param>
        /// <param name="InsuranceType"></param>
        /// <param name="InvalidFlag"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int PsBasicInfoSetData(DataConnection pclsCache, string UserId, string UserName, int Birthday, int Gender, int BloodType, string IDNo, string DoctorId, string InsuranceType, int InvalidFlag, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int IsSaved = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return IsSaved;

                }
                IsSaved = (int)Ps.BasicInfo.SetData(pclsCache.CacheConnectionObject, UserId, UserName, Birthday, Gender, BloodType, IDNo, DoctorId, InsuranceType, InvalidFlag, revUserId, TerminalName, TerminalIP, DeviceType);

                return IsSaved;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.PsBasicInfoSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return IsSaved;
            }
            finally
            {
                pclsCache.DisConnect();
            }


        }

        /// <summary>
        /// GetUserBasicInfo TDY 2014-12-4  //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserBasicInfo GetUserBasicInfo(DataConnection pclsCache, string UserId)
        {
            UserBasicInfo ret = new UserBasicInfo();
            try
            {

                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId);
                if (list != null)
                {
                    ret.UserName = list[0];
                    ret.Birthday = list[1];
                    ret.Gender = list[2];
                    ret.BloodType = list[3];
                    ret.IDNo = list[4];
                    ret.DoctorId = list[5];
                    ret.InsuranceType = list[6];
                    ret.InvalidFlag = list[7];
                }
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetUserBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        /// <summary>
        /// GetBasicInfo WF 2014-12-2  //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public PatientBasicInfo GetPatientBasicInfo(DataConnection pclsCache, string UserId)
        {
            PatientBasicInfo ret = new PatientBasicInfo();
            try
            {

                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.BasicInfo.GetPatientBasicInfo(pclsCache.CacheConnectionObject, UserId);
                if (list != null)
                {
                    ret.UserName = list[0];
                    ret.Age = list[1];
                    ret.Gender = list[2];
                    ret.BloodType = list[3];
                    ret.IDNo = list[4];
                    ret.DoctorId = list[5];
                    ret.InsuranceType = list[6];
                    ret.Birthday = list[7];
                    ret.GenderText = list[8];
                    ret.BloodTypeText = list[9];
                    ret.InsuranceTypeText = list[10];
                }
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetPatientBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }
        /// <summary>
        /// GetAgeByBirthDay SYF 2015-04-23 //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Birthday"></param>
        /// <returns></returns>
        public int GetAgeByBirthDay(DataConnection pclsCache, int Birthday)
        {
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return 0;
                }
                else
                {
                    int Age = (int)Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Birthday);
                    return Age;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetAgeByBirthDay", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return 0;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }

        /// <summary>
        /// GetBasicInfo WF 2014-12-2  //WF 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public BasicInfo GetBasicInfo(DataConnection pclsCache, string UserId)
        {
            BasicInfo ret = new BasicInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.BasicInfo.GetBasicInfo(pclsCache.CacheConnectionObject, UserId);
                if (list != null)
                {
                    ret.UserName = list[0];
                    ret.Birthday = list[1];
                    ret.IDNo = list[2];
                    ret.Gender = list[3];
                }
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetBasicInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }

        }

        //public bool BasicInfoDetailSetData(DataConnection pclsCache, string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        //{
        //    bool IsSaved = false;
        //    try
        //    {
        //        if (!pclsCache.Connect())
        //        {
        //            //MessageBox.Show("Cache数据库连接失败");
        //            return IsSaved;

        //        }
        //        int flag = (int)Ps.BasicInfoDetail.SetData(pclsCache.CacheConnectionObject, Patient, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, revUserId, TerminalName, TerminalIP, DeviceType);
        //        if (flag == 1)
        //        {
        //            IsSaved = true;
        //        }
        //        return IsSaved;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString(), "保存失败！");
        //        HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.BasicInfoDetailSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
        //        return IsSaved;
        //    }
        //    finally
        //    {
        //        pclsCache.DisConnect();
        //    }


        //}

        #endregion

        #region<PsDoctorInfo>

        /// <summary>
        /// GetModuleByDoctorId WF 2015-06-04 //syf 20151027
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetModuleByDoctorId(DataConnection pclsCache, string UserId)
        {
            string Module = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;

                }
                Module = Ps.DoctorInfo.GetModuleByDoctorId(pclsCache.CacheConnectionObject, UserId);

                return Module;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetModuleByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return Module;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="UserName"></param>
        /// <param name="Birthday"></param>
        /// <param name="Gender"></param>
        /// <param name="IDNo"></param>
        /// <param name="InvalidFlag"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        public int PsDoctorInfoSetData(DataConnection pclsCache, string UserId, string UserName, int Birthday, int Gender, string IDNo, int InvalidFlag, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            int IsSaved = 2;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return IsSaved;

                }
                IsSaved = (int)Ps.DoctorInfo.SetData(pclsCache.CacheConnectionObject, UserId, UserName, Birthday, Gender, IDNo, InvalidFlag, piUserId, piTerminalName, piTerminalIP, piDeviceType);

                return IsSaved;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.PsDoctorInfoSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return IsSaved;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        ///  GetDoctorInfo返回医生基本信息 ZYF 2014-12-4 //王丰 20151010
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public DoctorInfo GetDoctorInfo(DataConnection pclsCache, string DoctorId)
        {
            DoctorInfo ret = new DoctorInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.DoctorInfo.GetDoctorInfo(pclsCache.CacheConnectionObject, DoctorId);
                if (list != null)
                {
                    ret.DoctorId = list[0];
                    ret.DoctorName = list[1];
                    ret.Birthday = list[2];
                    ret.Gender = list[3];
                    ret.IDNo = list[4];
                    ret.InvalidFlag = list[5];
                }
                //DataCheck ZAM 2015-1-7
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetDoctorInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //GetCategoryByDoctorId 获取某个医生所有的详细信息分类及项目 SYF 2015-10-22
        public List<CategoryByDoctorId> GetCategoryByDoctorId(DataConnection pclsCache, string DoctorId)
        {
            List<CategoryByDoctorId> items = new List<CategoryByDoctorId>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.DoctorInfo.GetCategoryByDoctorId(pclsCache.CacheConnectionObject);
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    CategoryByDoctorId item = new CategoryByDoctorId();
                    item.CategoryCode = cdr["CategoryCode"].ToString();
                    item.CategoryName = cdr["CategoryName"].ToString();
                    item.ItemCode     = cdr["ItemCode"].ToString();
                    item.ItemName     = cdr["ItemName"].ToString();
                    item.Value        = cdr["Value"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetCategoryByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// 获取健康专员列表 SYF 20151022
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <returns></returns>
        public List<HealthCoachList> GetHealthCoachList(DataConnection pclsCache)
        {
            List<HealthCoachList> list = new List<HealthCoachList>();

            List<ActiveUser> list1 = new List<ActiveUser>();
            List<CategoryByDoctorId> list2 = new List<CategoryByDoctorId>();
            string moudlecodes = "";
            string[] moudlecode = null;
            string DoctorId = "";
            try
            {
                list1 = GetActiveUserByRole(pclsCache, "HealthCoach");//根据角色获取已激活的用户
                if (list1 != null)
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        DoctorInfo dcf = new DoctorInfo();
                        HealthCoachList hcf = new HealthCoachList();
                        //一次循环取一个健康专员的信息
                        DoctorId = list1[i].UserId;

                        dcf = new UsersMethod().GetDoctorInfo(pclsCache, DoctorId);//获取基本信息
                        hcf.healthCoachID = DoctorId;
                        hcf.name = dcf.DoctorName;
                        hcf.sex = dcf.Gender;
                        hcf.age = Convert.ToString(new UsersMethod().GetAgeByBirthDay(pclsCache, Convert.ToInt32(dcf.Birthday)));

                        moudlecodes = new UsersMethod().GetModuleByDoctorId(pclsCache, DoctorId);
                        if (moudlecodes != null)
                        {
                            moudlecode = moudlecodes.Split(new char[] { '_' });
                            for (int k = 0; k < moudlecode.Length; k++)
                            {
                                if (k == 0)
                                {
                                    hcf.module = new UsersMethod().GetCategoryName(pclsCache, moudlecode[k]);
                                }
                                else
                                {
                                    hcf.module = hcf.module + "/" + new UsersMethod().GetCategoryName(pclsCache, moudlecode[k]);
                                }
                            }
                        }
                        list2 = new UsersMethod().GetCategoryByDoctorId(pclsCache, DoctorId);
                        //获取某个健康专员的所有CategoryCode信息
                        if (list2 != null)
                        {
                            #region
                            for (int j = 0; j < list2.Count; j++)
                            {
                                if ((list2[j].CategoryCode == "Contact") && (list2[j].ItemCode == "Contact001_4"))
                                {
                                    hcf.imageURL = list2[j].Value;
                                }
                                //获取头像
                                else if ((list2[j].CategoryCode == "Score") && (list2[j].ItemCode == "Score_1"))
                                {
                                    hcf.score = list2[j].Value;
                                }
                                //获取该专员总体评分
                            }
                            #endregion
                        }
                        list.Add(hcf);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.GetHealthCoachList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 获取某专员相关信息 SYF 20151022
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="HealthCoachID"></param>
        /// <returns></returns>
        public HealthCoachInfo GetHealthCoachInfo(DataConnection pclsCache, string HealthCoachID)
        {
            HealthCoachInfo ret = new HealthCoachInfo();
            try
            {
                DoctorInfo ret1 = GetDoctorInfo(pclsCache, HealthCoachID);//获取基本信息

                ret.name = ret1.DoctorName;
                ret.sex = ret1.Gender;
                //ret.age = Convert.ToString(Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Convert.ToInt32(ret1.Birthday)));
                ret.age = Convert.ToString(new UsersMethod().GetAgeByBirthDay(pclsCache, Convert.ToInt32(ret1.Birthday)));
                string moudlecodes = "";
                string[] moudlecode = null;
                moudlecodes = new UsersMethod().GetModuleByDoctorId(pclsCache, HealthCoachID);
                if (moudlecodes != null)
                {
                    moudlecode = moudlecodes.Split(new char[] { '_' });
                    for (int k = 0; k < moudlecode.Length; k++)
                    {
                        if (k == 0)
                        {
                            ret.module = new UsersMethod().GetCategoryName(pclsCache, moudlecode[k]);
                        }
                        else
                        {
                            ret.module = ret.module + "/" + new UsersMethod().GetCategoryName(pclsCache, moudlecode[k]);
                        }
                    }
                }

                List<CategoryByDoctorId> ret2 = new List<CategoryByDoctorId>();
                ret2 = new UsersMethod().GetCategoryByDoctorId(pclsCache, HealthCoachID);
                if (ret2 != null)
                {
                    #region
                    for (int j = 0; j < ret2.Count; j++)
                    {
                        if ((ret2[j].CategoryCode == "Contact") && (ret2[j].ItemCode == "Contact001_4"))
                        {
                            ret.imageURL = ret2[j].Value;
                        }
                        //获取头像
                        else if ((ret2[j].CategoryCode == "Score") && (ret2[j].ItemCode == "Score_1"))
                        {
                            ret.generalscore = ret2[j].Value;
                        }
                        //获取该专员总体评分
                        else if ((ret2[j].CategoryCode == "Score") && (ret2[j].ItemCode == "activityDegree"))
                        {
                            ret.activityDegree = ret2[j].Value;
                        }
                        //获取该专员活跃度
                        else if ((ret2[j].CategoryCode == "Score") && (ret2[j].ItemCode == "generalComment"))
                        {
                            ret.generalComment = ret2[j].Value;
                        }
                        //获取该专员整体评价
                        else if ((ret2[j].CategoryCode == "Score") && (ret2[j].ItemCode == "patientNum"))
                        {
                            ret.patientNum = ret2[j].Value;
                        }
                        //获取该专员负责病人数量
                        else if ((ret2[j].CategoryCode == "Personal") && (ret2[j].ItemCode == "Description"))
                        {
                            ret.Description = ret2[j].Value;
                        }
                        //获取该专员的简介
                    }
                    #endregion
                }
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetDoctorInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //public bool DoctorInfoDetailSetData(DataConnection pclsCache, string Doctor, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        //{
        //    bool IsSaved = false;
        //    try
        //    {
        //        if (!pclsCache.Connect())
        //        {
        //            //MessageBox.Show("Cache数据库连接失败");
        //            return IsSaved;

        //        }
        //        int flag = (int)Ps.DoctorInfoDetail.SetData(pclsCache.CacheConnectionObject, Doctor, CategoryCode, ItemCode, ItemSeq, Value, Description, SortNo, piUserId, piTerminalName, piTerminalIP, piDeviceType);
        //        if (flag == 1)
        //        {
        //            IsSaved = true;
        //        }
        //        return IsSaved;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.ToString(), "保存失败！");
        //        HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.DoctorInfoDetailSetData", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
        //        return IsSaved;
        //    }
        //    finally
        //    {
        //        pclsCache.DisConnect();
        //    }
        //}

        #endregion

        #region<PsAppointment>
        /// <summary>
        /// 某患者预约某专员 //SYF 20151023 Ps.Appointment插入一条数据
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <param name="Module"></param>
        /// <param name="Description"></param>
        /// <param name="Status"></param>
        /// <param name="ApplicationTime"></param>
        /// <param name="AppointmentTime"></param>
        /// <param name="AppointmentAdd"></param>
        /// <param name="Redundancy"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int ReserveHealthCoach(DataConnection pclsCache, string DoctorId, string PatientId, string Module, string Description, int Status, DateTime ApplicationTime, DateTime AppointmentTime, string AppointmentAdd, string Redundancy, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Ps.Appointment.SetData(pclsCache.CacheConnectionObject, DoctorId, PatientId, Module, Description, Status, ApplicationTime, AppointmentTime, AppointmentAdd, Redundancy, revUserId, TerminalName, TerminalIP, DeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.ReserveHealthCoach", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        /// <summary>
        /// 更新预约状态
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <param name="Status"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int UpdateReservation(DataConnection pclsCache, string DoctorId, string PatientId, int Status, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret1 = 0;
            int ret2 = 0;
            int ret3 = 0;
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret1 = (int)Ps.Appointment.ChangeStatus(pclsCache.CacheConnectionObject, DoctorId, PatientId, Status, revUserId, TerminalName, TerminalIP, DeviceType);
                if ((ret1 == 1) && (Status == 3))
                {
                    int ItemSeq1 = (int)Ps.BasicInfoDetail.GetMaxItemSeq(pclsCache.CacheConnectionObject, PatientId, "M1", "Doctor");
                    ItemSeq1++;
                    int ItemSeq2 = (int)Ps.DoctorInfoDetail.GetMaxItemSeq(pclsCache.CacheConnectionObject, DoctorId, "M1", "Patient");
                    ItemSeq2++;
                    //可能以后还要判断有没有重复预约之类的
                    ret2 = (int)Ps.BasicInfoDetail.SetData(pclsCache.CacheConnectionObject, PatientId, "M1", "Doctor", ItemSeq1, DoctorId, "", 1, revUserId, TerminalName, TerminalIP, DeviceType);
                    ret3 = (int)Ps.DoctorInfoDetail.SetData(pclsCache.CacheConnectionObject, DoctorId, "M1", "Patient", ItemSeq2, PatientId, "", 1, revUserId, TerminalName, TerminalIP, DeviceType);
                    if ((ret3 == 1) && (ret2 == 1))
                    {
                        ret = 1;
                    }
                }
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.UpdateReservation", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        
        
        #endregion

        #region<Ps.Calendar>
        public List<Calendar> GetCalendar(DataConnection pclsCache, string DoctorId)
        {
            List<Calendar> list = new List<Calendar>();

            CacheCommand cmd = null;
            CacheDataReader cdr = null;

            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Calendar.GetCalendar(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    list.Add(new Calendar
                    {
                        DateTime = cdr["DateTime"].ToString(),
                        Period = cdr["Period"].ToString(),
                        SortNo = Convert.ToInt32(cdr["SortNo"]),
                        Description = cdr["Description"].ToString(),
                        Status = Convert.ToInt32(cdr["Status"]),
                        Redundancy = cdr["Redundancy"].ToString(),
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetCalendar", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        /// <summary>
        /// 获取某专员的评论列表 SYF 20151026
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<CommentList> GetCommentList(DataConnection pclsCache, string DoctorId, string CategoryCode)
        {
            List<CommentList> list = new List<CommentList>();
            List<DetailsByDoctorId> list1 = new List<DetailsByDoctorId>();
            try
            {
                list1 = GetDetailsByDoctorId(pclsCache, DoctorId, CategoryCode);//获取健康专员所负责的病人对他的评价及评分信息
                if (list1 != null)
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        CommentList clt = new CommentList();
                        clt.PatientId = list1[i].PatientId;
                        clt.Name = list1[i].Name;
                        clt.Comment = list1[i].Comment;
                        clt.Score = list1[i].Score;
                        clt.CommentTime = list1[i].CommentTime;

                        if (clt.PatientId != "")
                        {
                            //PatientDetailInfo1 ret = new PatientDetailInfo1();
                            GetPatientDetailInfo lt = new GetPatientDetailInfo();
                            lt = GetPatientDetailInfo(pclsCache, clt.PatientId);
                            if (lt != null)
                            {
                                clt.imageURL = lt.PhotoAddress;
                            }
                        }
                        list.Add(clt);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.GetCommentList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        /// <summary>
        /// GetCategoryName WF 2014-12-2 //syf 20151027
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetCategoryName(DataConnection pclsCache, string Code)
        {
            string ret = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return ret;

                }
                ret = (Cm.MstInfoItemCategory.GetCategoryName(pclsCache.CacheConnectionObject, Code)).ToString();
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetCategoryName", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        /// <summary>
        ///  获取需要知道的项目的值 ZC 20150603 //syf20151027
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="ItemSeq"></param>
        /// <returns></returns>
        public string GetValue(DataConnection pclsCache, string UserId, string CategoryCode, string ItemCode, int ItemSeq)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return ret;

                }
                ret = (string)Ps.DoctorInfoDetail.GetValue(pclsCache.CacheConnectionObject, UserId, CategoryCode, ItemCode, ItemSeq);
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetValue", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }


        //GetDoctorsByPatientId 根据PatientId和CategoryCode取对应模块医生列表 SYF 2015-10-26
        public List<DoctorsByPatientId> GetDoctorsByPatientId(DataConnection pclsCache, string PatientId, string CategoryCode)
        {
            List<DoctorsByPatientId> items = new List<DoctorsByPatientId>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.BasicInfoDetail.GetDoctorsByPatientId(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("PatientId", CacheDbType.NVarChar).Value = PatientId;
                cmd.Parameters.Add("CategoryCode", CacheDbType.NVarChar).Value = CategoryCode;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    DoctorsByPatientId item = new DoctorsByPatientId();
                    item.DoctorId = cdr["DoctorId"].ToString();
                    item.DoctorName = cdr["DoctorName"].ToString();
                    item.ItemSeq = cdr["ItemSeq"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetDoctorsByPatientId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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

        //GetPatientDetailInfo TDY 2015-04-07
        public GetPatientDetailInfo GetPatientDetailInfo(DataConnection pclsCache, string UserId)
        {
            GetPatientDetailInfo ret = new GetPatientDetailInfo();

            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.BasicInfoDetail.GetDetailInfo(pclsCache.CacheConnectionObject, UserId);
                if (list != null)
                {
                    ret.PhoneNumber = list[0];
                    ret.HomeAddress = list[1];
                    ret.Occupation = list[2];
                    ret.Nationality = list[3];
                    ret.EmergencyContact = list[4];
                    ret.EmergencyContactPhoneNumber = list[5];
                    ret.PhotoAddress = list[6];
                    ret.IDNo = list[7];
                }
                //DataCheck ZAM 2015-1-7
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetPatientDetailInfo", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        //GetDetailsByDoctorId 获取健康专员所负责的病人对他的评价及评分信息 SYF 2015-10-26
        public List<DetailsByDoctorId> GetDetailsByDoctorId(DataConnection pclsCache, string DoctorId, string CategoryCode)
        {
            List<DetailsByDoctorId> items = new List<DetailsByDoctorId>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.DoctorInfoDetail.GetDetailsByDoctorId(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;
                cmd.Parameters.Add("CategoryCode", CacheDbType.NVarChar).Value = CategoryCode;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    DetailsByDoctorId item = new DetailsByDoctorId();
                    item.PatientId = cdr["PatientId"].ToString();
                    item.Name = cdr["Name"].ToString();
                    item.Comment = cdr["Comment"].ToString();
                    item.Score = cdr["Score"].ToString();
                    item.CommentTime = cdr["CommentTime"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetDetailsByDoctorId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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



        public List<HealthCoachListByPatient> GetHealthCoachListByPatient(DataConnection pclsCache, string PatientId, string CategoryCode)
        {
            List<HealthCoachListByPatient> list = new List<HealthCoachListByPatient>();
            List<DoctorsByPatientId> list1 = new List<DoctorsByPatientId>();
            try
            {
                list1 = GetDoctorsByPatientId(pclsCache, PatientId, CategoryCode);//根据PatientId和CategoryCode取对应模块医生列表
                if (list1 != null)
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        HealthCoachListByPatient lt = new HealthCoachListByPatient();
                        lt.HealthCoachID = list1[i].DoctorId;//专员Id
                        lt.Name = list1[i].DoctorName;//专员名字
                        lt.imageURL = new UsersMethod().GetValue(pclsCache, lt.HealthCoachID, "Contact", "Contact001_4", 1);//专员照片
                        lt.module = new UsersMethod().GetCategoryName(pclsCache, CategoryCode);//负责模块，由输入决定
                        if (lt.HealthCoachID != "")
                        {
                            lt.latestMessage = new MessageMethod().GetLatestSMS(pclsCache, lt.HealthCoachID, PatientId);//最新消息
                        }
                        list.Add(lt);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.GetCommentList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }





        /// <summary>
        /// 解除专员 syf 20151026
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="PatientId"></param>
        /// <param name="DoctorId"></param>
        /// <param name="Category"></param>
        /// <returns></returns>
        public int RemoveHealthCoach(DataConnection pclsCache, string PatientId, string DoctorId, string CategoryCode)
        {
            int ret = 0;
            int Status = -1;
            GPlanInfo ret1 = new GPlanInfo();
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Ps.Plan.GetExecutingPlan(pclsCache.CacheConnectionObject, PatientId);
                if (list != null)//病人有正在执行的计划，但是没有对应该医生的正在执行的计划
                {
                    if ((ret1.Status != "3") && (ret1.DoctorId == DoctorId))//该病人对于当前医生来说，无正在执行的计划
                    {
                        Status = (int)Ps.Appointment.GetStatusById(pclsCache.CacheConnectionObject, DoctorId, PatientId);//当Ps.Appointment预约表的Status为0时才删除
                        if (Status == 0)
                        {
                            ret = (int)Ps.Appointment.Delete(pclsCache.CacheConnectionObject, DoctorId, PatientId);
                            ret = (int)Ps.BasicInfoDetail.DeleteDataByItemCode(pclsCache.CacheConnectionObject, PatientId, CategoryCode, "Doctor", DoctorId);
                            ret = (int)Ps.DoctorInfoDetail.DeleteDataByItemCode(pclsCache.CacheConnectionObject, DoctorId, CategoryCode, "Patient", PatientId);
                        }
                    }
                }
                else
                {
                    //该病人无正在执行的计划
                    Status = (int)Ps.Appointment.GetStatusById(pclsCache.CacheConnectionObject, DoctorId, PatientId);//当Ps.Appointment预约表的Status为0时才删除
                    if (Status == 0)
                    {
                        ret = (int)Ps.Appointment.Delete(pclsCache.CacheConnectionObject, DoctorId, PatientId);
                        string Doctor_Id = " " + DoctorId.ToUpper();//数据库的数据Value这一项加了个空格,而且小写变大写
                        string Patient_Id = " " + PatientId.ToUpper();
                        ret = (int)Ps.BasicInfoDetail.DeleteDataByItemCode(pclsCache.CacheConnectionObject, PatientId, CategoryCode, "Doctor", Doctor_Id);
                        ret = (int)Ps.DoctorInfoDetail.DeleteDataByItemCode(pclsCache.CacheConnectionObject, DoctorId, CategoryCode, "Patient", Patient_Id);
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.RemoveHealthCoach", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        public List<PatientsByStatus> GetPatientsByStatus(DataConnection pclsCache, string DoctorId, string Status)
        {
            List<PatientsByStatus> items = new List<PatientsByStatus>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Appointment.GetPatientsByStatus(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;
                cmd.Parameters.Add("Status", CacheDbType.NVarChar).Value = Status;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    PatientsByStatus item = new PatientsByStatus();
                    item.PatientId = cdr["PatientId"].ToString();
                    item.Module = cdr["Module"].ToString();
                    item.Status = cdr["Status"].ToString();
                    item.Description = cdr["Description"].ToString();
                    item.ApplicationTime = cdr["ApplicationTime"].ToString();
                    item.AppointmentTime = cdr["AppointmentTime"].ToString();
                    item.AppointmentAdd = cdr["AppointmentAdd"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetPatientsByStatus", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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


        /// <summary>
        /// 获取需要知道的项目的值 SYF 20150423 syf 20151027
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <param name="CategoryCode"></param>
        /// <param name="ItemCode"></param>
        /// <param name="ItemSeq"></param>
        /// <returns></returns>
        public string GetPatientValue(DataConnection pclsCache, string UserId, string CategoryCode, string ItemCode, int ItemSeq)
        {
            string ret = "";
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return ret;

                }
                ret = (string)Ps.BasicInfoDetail.GetValue(pclsCache.CacheConnectionObject, UserId, CategoryCode, ItemCode, ItemSeq);
                return ret;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "保存失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UsersMethod.GetPatientValue", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }



        public List<AppoitmentPatient> GetAppoitmentPatientList(DataConnection pclsCache, string healthCoachID, string Status)
        {
            List<AppoitmentPatient> list = new List<AppoitmentPatient>();
            List<PatientsByStatus> list1 = new List<PatientsByStatus>();
            try
            {
                list1 = GetPatientsByStatus(pclsCache, healthCoachID, Status);
                if (list1 != null)
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        AppoitmentPatient app = new AppoitmentPatient();
                        app.PatientID = list1[i].PatientId;
                        app.module = list1[i].Module;
                        app.AppointmentStatus = list1[i].Status;
                        app.Description = list1[i].Description;
                        app.ApplicationTime = list1[i].ApplicationTime;
                        app.ApplicationTime = list1[i].ApplicationTime;
                        app.AppointmentAdd = list1[i].AppointmentAdd;

                        if (app.PatientID != "")
                        {
                            //list[i].imageURL = Ps.DoctorInfoDetail.GetValue(pclsCache.CacheConnectionObject, app.PatientID, "Contact", "Contact001_4", 1);
                            app.imageURL = new UsersMethod().GetPatientValue(pclsCache, app.PatientID, "Contact", "Contact001_4", 1);//病人照片
                            UserBasicInfo ret = new UserBasicInfo();
                            ret = new UsersMethod().GetUserBasicInfo(pclsCache, app.PatientID);
                            if (ret != null)
                            {
                                app.name = ret.UserName;

                                app.age = ret.Age;
                                //app.age = Convert.ToString(Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Convert.ToInt32(ret.Birthday)));

                                app.sex = ret.Gender;
                            }
                        }
                        list.Add(app);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "UserMethod.GetAppoitmentPatientList", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 根据ID获取手机号 LY 2015-10-29
        /// </summary>
        /// <param name="pclsCache"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetPhoneNoByUserId(DataConnection pclsCache, string UserId)
        {
            try
            {
                if (!pclsCache.Connect())
                {
                    //MessageBox.Show("Cache数据库连接失败");
                    return null;
                }
                string Name = Cm.MstUser.GetPhoneNoByUserId(pclsCache.CacheConnectionObject, UserId);
                return Name;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), "获取名称失败！");
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "CmMstUser.GetPhoneNoByUserId", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        #region 第二层
        public int RegisterRelated(DataConnection pclsCache, string PwType,string userId,string Password, string UserName,string role,string revUserId,string TerminalName,string  TerminalIP,int DeviceType)
        {
            int ret = 0;
            int Flag1 = Register(pclsCache, PwType, userId, "", Password, UserName, revUserId, TerminalName, TerminalIP, DeviceType);
            //数据库Cm.MstUser和Cm.MstUserDetail表数据写入成功
            if (Flag1 == 1)
            {
                string userID = GetIDByInput(pclsCache, PwType, userId);
                if (userID != "")
                {
                    string InviteNo = new CommonMethod().GetNo(pclsCache, 12, "");
                    if (InviteNo != "")
                    {
                        int test = PsRoleMatchSetData(pclsCache, userID, role, InviteNo, "1", "");
                        if (test == 1)
                        {
                            ret = 1;//"注册成功";
                        }
                        else
                        {
                            ret = 2; // "注册失败！";
                        }
                    }
                }
            }
            return ret;
        }
        
        #endregion
    }
}