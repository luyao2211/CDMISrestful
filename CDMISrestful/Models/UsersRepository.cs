using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CDMISrestful.CommonLibrary;
using CDMISrestful.DataMethod;
using InterSystems.Data.CacheClient;

namespace CDMISrestful.Models
{
    public class UsersRepository : IUsersRepository
    {
        //DataConnection
        DataConnection pclsCache = new DataConnection();
        UsersMethod usersMethod = new UsersMethod();
        CommonMethod commonMethod = new CommonMethod();

        public string IsUserValid(string userId, string password)
        {
            if (userId != "" && password != "")
            {
                string PwType = "PhoneNo";
                int result = usersMethod.CheckPasswordByInput(pclsCache, PwType, userId, password);
                if (result == 1)
                {
                    return SecurityManager.GenerateToken(userId, password);
                }
                else
                {
                    return "不合法用户";
                }
            }
            else
            {
                return "Invalid Input";
            }
        }
        public int LogOn(string PwType, string userId, string password, string role)
        {
            int result = usersMethod.CheckPasswordByInput(pclsCache, PwType, userId, password);
            if (result == 1)
            {
                //密码验证成功
                string UId = usersMethod.GetIDByInput(pclsCache, PwType, userId); //输入手机号获取用户ID
                if (UId != "")
                {
                    string Class = usersMethod.GetActivatedState(pclsCache, UId, role);
                    if (Class == "0")
                    {
                        int flag = 0;
                        List<string> AllRoleMatch = usersMethod.GetAllRoleMatch(pclsCache, UId);
                        for (int i = 0; i < AllRoleMatch.Count; i++)
                        {
                            if (AllRoleMatch[i].ToString() == role)//查询条件
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            return 1; //"已注册激活且有权限，登陆成功，跳转到主页";
                        }
                        else
                        {
                            return 2; //"已注册激活 但没有权限";
                        }
                    }
                    else      //Class == "1" or Class == ""
                    {
                        return 3;            //您的账号对应的角色未激活，需要先激活；界面跳转到游客页面（已注册但未激活）
                    }
                }
                else
                {
                    return 4; //"用户不存在";
                }
            }
            else if (result == 0)
            {
                return 5; //"密码错误";
            }
            else
            {
                return 4;   //"用户不存在"
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PwType"></param>
        /// <param name="userId">手机号/邮箱等</param>
        /// <param name="UserName">用户姓名</param>
        /// <param name="Password"></param>
        /// <param name="role"></param>
        /// <param name="revUserId"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TerminalIP"></param>
        /// <param name="DeviceType"></param>
        /// <returns></returns>
        public int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            //userID：系统唯一标识

            int roleFlag = 0;
            int ret = 0;
            int Flag = usersMethod.CheckRepeat(pclsCache, userId, PwType);
            //用户名不存在
            if (Flag == 1)
            {
                ret = usersMethod.RegisterRelated(pclsCache, PwType, userId, Password, UserName, role, revUserId, TerminalName, TerminalIP, DeviceType);
            }
            //用户名已存在
            else
            {            
                string UserId = usersMethod.GetIDByInput(pclsCache, PwType, userId);
                //系统唯一标识已产生
                if (UserId != "")
                {
                    List<string> result = usersMethod.GetAllRoleMatch(pclsCache, UserId);
                    //该用户已有角色信息
                    if (result != null)
                    {
                        int flag3 = 0;
                        for (int i = 0; i < result.Count(); i++)
                        {
                            string Role = result[i];
                            if (Role == role)
                            {
                                flag3 = 1;
                                roleFlag = 1;
                            }
                            if (flag3 == 1)
                            {
                                //同一用户名的同一角色已经存在
                                ret = 3; 
                            }

                        }
                        //该角色还没有想要创建的角色
                        if (roleFlag == 0)
                        {
                            string InviteNo = commonMethod.GetNo(pclsCache, 12, "");
                            if (InviteNo != "")
                            {
                                int test = usersMethod.PsRoleMatchSetData(pclsCache, UserId, role, InviteNo, "1", "");
                                if (test == 1)
                                {
                                    ret = 4; // "新建角色成功，密码与您已有账号一致";
                                }
                                else
                                {
                                    ret = 2; // "注册失败！";
                                }
                            }
                        }
                       
                    }                
                }
                //输入的用户名还没有系统唯一标识：在Cm.MstUser表中数据写入成功后，Cm.MstUserDetail表数据写入失败/一些旧用户在创建时只写Cm.MstUser表，而没有写Cm.MstUserDetail表
                else
                {
                    ret = usersMethod.RegisterRelated(pclsCache, PwType, userId, Password, UserName, role, revUserId, TerminalName, TerminalIP, DeviceType);
                }                        
            }
            return ret;
        }

        public int Activition(string UserId, string InviteCode, string role)
        {
            int ret = 0;
            int Flag = usersMethod.SetActivition(pclsCache, UserId, role, InviteCode);
            if (Flag == 1)
            {
                ret = 1; // "激活成功";
            }
            else
            {
                ret = 2; // "激活失败";
            }
            return ret;
        }

        public int ChangePassword(string OldPassword, string NewPassword, string UserId, string revUserId, string TerminalName, string TerminalIP, int DeviceType)
        {
            int ret = 0;
            ret = usersMethod.ChangePassword(pclsCache, UserId, OldPassword, NewPassword, revUserId, TerminalName, TerminalIP, DeviceType);
            return ret;
        }
		
        
    }
}