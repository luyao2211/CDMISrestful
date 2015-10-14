using CDMISrestful.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDMISrestful.Models
{
    public interface IModuleInfoRepository
    {
        List<PatBasicInfoDetail> GetItemInfoByPIdAndModule(string UserId, string CategoryCode);
       
        int SetPatBasicInfoDetail(string Patient, string CategoryCode, string ItemCode, int ItemSeq, string Value, string Description, int SortNo, string revUserId, string TerminalName, string TerminalIP, int DeviceType);
        List<MstInfoItemByCategoryCode> GetMstInfoItemByCategoryCode(string CategoryCode);
        SynBasicInfo SynBasicInfoDetail(string UserId);
    }
}
