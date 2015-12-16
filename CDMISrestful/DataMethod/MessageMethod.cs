﻿using CDMISrestful.CommonLibrary;
using CDMISrestful.DataModels;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataMethod
{
    public class MessageMethod
    {
        #region < "Mb.MessageRecord" >
        /// <summary>
        /// 获取消息对话 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public List<Message> GetSMSDialogue(DataConnection pclsCache, string Reciever, string SendBy)
        {
            List<Message> items = new List<Message>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = Mb.MessageRecord.GetSMSDialogue(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("Reciever", CacheDbType.NVarChar).Value = Reciever;
                cmd.Parameters.Add("SendBy", CacheDbType.NVarChar).Value = SendBy;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    Message item = new Message();
                    item.Time = cdr["Time"].ToString();
                    item.Content = cdr["Content"].ToString();
                    item.IDFlag = cdr["IDFlag"].ToString();
                    item.SendDateTime = cdr["SendDateTime"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.GetSMSDialogue", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// 将消息写入数据库并获取发送时间与显示时间 GL 2015-10-10（修改2015-10-26）
        /// </summary>
        /// <param name="SendBy"></param>
        /// <param name="Reciever"></param>
        /// <param name="Content"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        public Message SetSMS(DataConnection pclsCache, string SendBy, string Reciever, string Content, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            try
            {
                Message Meg = new Message();
                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Mb.MessageRecord.SetSMS(pclsCache.CacheConnectionObject, SendBy, Reciever, Content, piUserId, piTerminalName, piTerminalIP, piDeviceType);
                if (list != null)
                {
                    Meg.Flag = list[0];
                    Meg.SendDateTime = list[1];
                    Meg.Time = list[2];
                }
                return Meg;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.SetSMS", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }           
        }

        /// <summary>
        /// 获取最新一条消息 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="PatientId"></param>
        /// <returns></returns>
        public Message GetLatestSMS(DataConnection pclsCache, string DoctorId, string PatientId)
        {
            try
            {
                Message Meg = new Message();
                if (!pclsCache.Connect())
                {
                    return null;
                }
                InterSystems.Data.CacheTypes.CacheSysList list = null;
                list = Mb.MessageRecord.GetLatestSMS(pclsCache.CacheConnectionObject, DoctorId, PatientId);
                if (list != null)
                {
                    Meg.MessageNo = list[0];
                    Meg.Content = list[1];
                    Meg.SendDateTime = list[2];
                    Meg.SendByName = list[3];
                    Meg.Flag = list[4];
                }
                return Meg;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.GetLatestSMS", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 将多条消息设为已读 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <param name="piUserId"></param>
        /// <param name="piTerminalName"></param>
        /// <param name="piTerminalIP"></param>
        /// <param name="piDeviceType"></param>
        /// <returns></returns>
        public int SetSMSRead(DataConnection pclsCache, string Reciever, string SendBy, string piUserId, string piTerminalName, string piTerminalIP, int piDeviceType)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Mb.MessageRecord.SetSMSRead(pclsCache.CacheConnectionObject, Reciever, SendBy, piUserId, piTerminalName, piTerminalIP, piDeviceType);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.SetSMSRead", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 获取一对一未读消息数 GL 2015-10-10
        /// </summary>
        /// <param name="Reciever"></param>
        /// <param name="SendBy"></param>
        /// <returns></returns>
        public int GetSMSCountForOne(DataConnection pclsCache, string Reciever, string SendBy)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Mb.MessageRecord.GetSMSCountForOne(pclsCache.CacheConnectionObject, Reciever, SendBy);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.GetSMSCountForOne", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }

        /// <summary>
        /// 获取消息联系人列表 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <param name="CategoryCode"></param>
        /// <returns></returns>
        public List<Message> GetSMSList(DataConnection pclsCache, string DoctorId, string CategoryCode)
        {
            List<Message> items = new List<Message>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = Mb.MessageRecord.GetSMSList(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("DoctorId", CacheDbType.NVarChar).Value = DoctorId;
                cmd.Parameters.Add("CategoryCode", CacheDbType.NVarChar).Value = CategoryCode;
                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    //PatientId, PatientName, Count, Content, SendDateTime
                    Message item = new Message();
                    item.SendBy = cdr["PatientId"].ToString();
                    item.SendByName = cdr["PatientName"].ToString();
                    item.Count = cdr["Count"].ToString();
                    item.Content = cdr["Content"].ToString();
                    item.SendDateTime = cdr["SendDateTime"].ToString();
                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.GetSMSDialogue", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
        /// 获取某医生未读消息总数 GL 2015-10-10
        /// </summary>
        /// <param name="DoctorId"></param>
        /// <returns></returns>
        public int GetSMSCountForAll(DataConnection pclsCache, string DoctorId)
        {
            int ret = 0;
            try
            {
                if (!pclsCache.Connect())
                {
                    return ret;
                }

                ret = (int)Mb.MessageRecord.GetSMSCountForAll(pclsCache.CacheConnectionObject, DoctorId);
                return ret;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.GetSMSCountForAll", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
                return ret;
            }
            finally
            {
                pclsCache.DisConnect();
            }
        }
        
        #endregion
       
       public List<PsNotification> PsNotificationGetDataByStatus(DataConnection pclsCache, string AccepterID, string NotificationType, string Status)
        {
            if (Status == "{Status}")
                Status = "-1";
            List<PsNotification> items = new List<PsNotification>();
            CacheCommand cmd = null;
            CacheDataReader cdr = null;
            try
            {
                if (!pclsCache.Connect())
                {
                    return null;
                }
                cmd = new CacheCommand();
                cmd = Ps.Notification.GetDataByStatus(pclsCache.CacheConnectionObject);
                cmd.Parameters.Add("AccepterID", CacheDbType.NVarChar).Value = AccepterID;
                cmd.Parameters.Add("NotificationType", CacheDbType.NVarChar).Value = NotificationType;
                cmd.Parameters.Add("Status", CacheDbType.NVarChar).Value = Status;

                cdr = cmd.ExecuteReader();
                while (cdr.Read())
                {
                    PsNotification item = new PsNotification();
                    item.AccepterID = cdr["AccepterID"].ToString();
                    item.NotificationType = cdr["NotificationType"].ToString();
                    item.SortNo = cdr["SortNo"].ToString();
                    item.Title = cdr["Title"].ToString();
                    item.Description = cdr["Description"].ToString();
                    item.SendTime = cdr["SendTime"].ToString();
                    item.SenderID = cdr["SenderID"].ToString();
                    item.Status = cdr["Status"].ToString();

                    items.Add(item);
                }
                return items;
            }
            catch (Exception ex)
            {
                HygeiaComUtility.WriteClientLog(HygeiaEnum.LogType.ErrorLog, "MessageMethod.PsNotificationGetDataByStatus", "数据库操作异常！ error information : " + ex.Message + Environment.NewLine + ex.StackTrace);
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
    
    }
}