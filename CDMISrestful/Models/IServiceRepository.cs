using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDMISrestful.Models
{
    public interface IServiceRepository
    {
        string sendSMS(string mobile, string smsType);
        int checkverification(string mobile, string smsType, string verification);
        string PushNotification(string platform, string Alias, string notification);
        List<TypeAndName> GetPatientInfo(string PatientId);
        int VitalSignFromZKY(VitalSignFromDevice VitalSigns, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
    }
}