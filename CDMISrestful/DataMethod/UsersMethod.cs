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
            list = null;
            List<ActiveUser> list1 = new List<ActiveUser>();
            List<CategoryByDoctorId> list2 = new List<CategoryByDoctorId>();
            List<DoctorInfo> list3 = new List<DoctorInfo>();
            string DoctorId = "";
            try
            {
                list1 = GetActiveUserByRole(pclsCache, "HealthCoach");
                if(list1 != null)
                {
                    for(int i=0;i<list1.Count;i++)
                    {
                        //一次循环取一个健康专员的信息
                        DoctorId = list1[i].UserId;
                        list3[i] = GetDoctorInfo(pclsCache, DoctorId);//获取基本信息

                        list[i].healthCoachID = DoctorId;
                        list[i].name = list3[i].DoctorName;
                        list[i].sex = list3[i].Gender;
                        list[i].age = Convert.ToString(Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Convert.ToInt32(list3[i].Birthday)));

                        list2 = GetCategoryByDoctorId(pclsCache, DoctorId);
                        //获取某个健康专员的所有CategoryCode信息
                        if(list2 != null)
                        {
                            #region
                            for (int j=0; j<list2.Count; j++)
                            {
                                if(list2[j].CategoryCode=="M1")
                                {
                                    list[i].module = list[i].module + "/" + "高血压模块";
                                }
                                else if(list2[j].CategoryCode=="M2")
                                {
                                    list[i].module = list[i].module + "/" + "糖尿病模块";
                                }
                                else if (list2[j].CategoryCode == "M3")
                                {
                                    list[i].module = list[i].module + "/" + "心力衰竭模块";
                                }
                                else if (list2[j].CategoryCode == "M4")
                                {
                                    list[i].module = list[i].module + "/" + "心律失常模块";
                                }
                                else if (list2[j].CategoryCode == "M5")
                                {
                                    list[i].module = list[i].module + "/" + "健康管理模块";
                                }
                                //获取某个健康专员的模块信息，多个模块信息用“/”拼接
                                else if( (list2[j].CategoryCode=="Contact")&&(list2[j].ItemCode=="Contact001_4") )
                                {
                                    list[i].imageURL = list2[j].Value;
                                }
                                //获取头像
                                else if ((list2[j].CategoryCode == "Score") && (list2[j].ItemCode == "Score_1"))
                                {
                                    list[i].imageURL = list2[j].Value;
                                }
                                //获取该专员总体评分
                            }
                            #endregion
                        }

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
                ret.age = Convert.ToString(Ps.BasicInfo.GetAgeByBirthDay(pclsCache.CacheConnectionObject, Convert.ToInt32(ret1.Birthday)));

                List<CategoryByDoctorId> ret2 = new List<CategoryByDoctorId>();
                ret2 = GetCategoryByDoctorId(pclsCache, HealthCoachID);
                if (ret2 != null)
                {
                    #region
                    for (int j = 0; j < ret2.Count; j++)
                    {
                        if (ret2[j].CategoryCode == "M1")
                        {
                            ret.module = ret.module + "/" + "高血压模块";
                        }
                        else if (ret2[j].CategoryCode == "M2")
                        {
                            ret.module = ret.module + "/" + "糖尿病模块";
                        }
                        else if (ret2[j].CategoryCode == "M3")
                        {
                            ret.module = ret.module + "/" + "心力衰竭模块";
                        }
                        else if (ret2[j].CategoryCode == "M4")
                        {
                            ret.module = ret.module + "/" + "心律失常模块";
                        }
                        else if (ret2[j].CategoryCode == "M5")
                        {
                            ret.module = ret.module + "/" + "健康管理模块";
                        }
                        //获取某个健康专员的模块信息，多个模块信息用“/”拼接
                        else if ((ret2[j].CategoryCode == "Contact") && (ret2[j].ItemCode == "Contact001_4"))
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
                if( (ret1 == 1)&&(Status == 3) )
                {
                    ret++;
                    int ItemSeq1 = (int)Ps.BasicInfoDetail.GetMaxItemSeq(pclsCache.CacheConnectionObject, PatientId, "M1", "Doctor");
                    ItemSeq1++;
                    int ItemSeq2 = (int)Ps.DoctorInfoDetail.GetMaxItemSeq(pclsCache.CacheConnectionObject, DoctorId, "M1", "Patient");
                    ItemSeq2++;
                    ret2 = (int)Ps.BasicInfoDetail.SetData(pclsCache.CacheConnectionObject, PatientId, "M1", "Doctor", ItemSeq1, DoctorId, "", 1, revUserId, TerminalName, TerminalIP, DeviceType);
                    if(ret2==1)
                    {
                        ret++;
                    }
                    ret3 = (int)Ps.DoctorInfoDetail.SetData(pclsCache.CacheConnectionObject, DoctorId, "M1", "Patient", ItemSeq2, PatientId, "", 1, revUserId, TerminalName, TerminalIP, DeviceType);
                    if(ret3==1)
                    {
                        ret++;
                    }
                }
                return ret; //0数据库连接失败；1预约表更改状态成功；2病人新增医生成功；3医生新增病人成功
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