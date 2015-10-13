using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IUsersRepository
    {
        int LogOn(string PwType, string userId, string password, string role);
        string IsUserValid(string userId, string password);
        int Register(string PwType, string userId, string UserName, string Password, string role, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        int Activition(string UserId, string InviteCode,string role);
        int ChangePassword(string OldPassword, string NewPassword, string UserId, string revUserId, string TerminalName, string TerminalIP, int DeviceType);

    }
}