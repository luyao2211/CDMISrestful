using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CDMISrestful.DataModels
{
    public class Users
    {
    }

    public class LogOn
    {

        [Required(ErrorMessage = "请传入Type")]
        public string PwType { get; set; }
        [Required(ErrorMessage = "请输入用户名")]
        //[RegularExpression(@"/^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/" @"/^1[3|4|5|7|8][0-9]\d{4,8}$/)]
        public string username { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string password { get; set; }
        [Required(ErrorMessage = "角色信息必填")]
        public string role { get; set; }
    }

    public class Register
    {
        [Required(ErrorMessage = "请传入Type")]
        public string PwType { get; set; }
        [Required(ErrorMessage = "请输入用户Id")]
        public string userId { get; set; }
        [Required(ErrorMessage = "请输入用户名")]
        //[RegularExpression(@"/^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/" @"/^1[3|4|5|7|8][0-9]\d{4,8}$/)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        [Required(ErrorMessage = "角色信息必填")]
        public string role { get; set; }

        [Required(ErrorMessage = "revUserId")]
        public string revUserId { get; set; }
        [Required(ErrorMessage = "TerminalName")]
        public string TerminalName { get; set; }
        [Required(ErrorMessage = "TerminalIP")]
        public string TerminalIP { get; set; }
        [Required(ErrorMessage = "DeviceType")]
        public int DeviceType { get; set; }
    }

    public class Activation
    {
        [Required(ErrorMessage = "请传入用户Id")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "请输入邀请码")]
        public string InviteCode { get; set; }
        [Required(ErrorMessage = "请输入角色")]
        public string role { get; set; }
    }
    public class ChangePassword
    {
        [Required(ErrorMessage = "请传入OldPassword")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "请传入NewPassword")]
        public string NewPassword { get; set; } 
        [Required(ErrorMessage = "请输入用户Id")]
        public string UserId { get; set; }    
        [Required(ErrorMessage = "revUserId")]
        public string revUserId { get; set; }
        [Required(ErrorMessage = "TerminalName")]
        public string TerminalName { get; set; }
        [Required(ErrorMessage = "TerminalIP")]
        public string TerminalIP { get; set; }
        [Required(ErrorMessage = "DeviceType")]
        public int DeviceType { get; set; }

    }

    public class GetPatientsList
    {
        [Required(ErrorMessage = "请传入DoctorId")]
        public string DoctorId { get; set; }
        [Required(ErrorMessage = "请传入ModuleType")]
        public string ModuleType { get; set; }
        [Required(ErrorMessage = "请传入Plan")]
        public int Plan { get; set; }
        [Required(ErrorMessage = "请输入Compliance")]
        public int Compliance { get; set; }
        [Required(ErrorMessage = "请输入Goal")]
        public int Goal { get; set; }


    }
    public class Verification
    {
        [Required(ErrorMessage = "请传入用户Id")]
        public string userId { get; set; }
        [Required(ErrorMessage = "请传入ValidateCode")]
        public string ValidateCode { get; set; }
        [Required(ErrorMessage = "请传入PwType")]
        public string PwType { get; set; }


    }
    public class ResetPassword
    {
        [Required(ErrorMessage = "请传入NewPassword")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "请传入ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "请传入UserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "请输入Key")]
        public string Key { get; set; }
        [Required(ErrorMessage = "请输入Device")]
        public string Device { get; set; }
        [Required(ErrorMessage = "revUserId")]
        public string revUserId { get; set; }
        [Required(ErrorMessage = "TerminalName")]
        public string TerminalName { get; set; }
        [Required(ErrorMessage = "TerminalIP")]
        public string TerminalIP { get; set; }
        [Required(ErrorMessage = "DeviceType")]
        public int DeviceType { get; set; }


    }
    public class SetDoctorInfoDetail
    {
        [Required(ErrorMessage = "请传入Doctor")]
        public string Doctor { get; set; }
        [Required(ErrorMessage = "请传入CategoryCode")]
        public string CategoryCode { get; set; }
        [Required(ErrorMessage = "请传入ItemCode")]
        public string ItemCode { get; set; }
        [Required(ErrorMessage = "请输入ItemSeq")]
        public int ItemSeq { get; set; }
        [Required(ErrorMessage = "请输入Value")]
        public string Value { get; set; }
        [Required(ErrorMessage = "请输入Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "请输入SortNo")]
        public int SortNo { get; set; }
        [Required(ErrorMessage = "piUserId")]
        public string piUserId { get; set; }
        [Required(ErrorMessage = "piTerminalName")]
        public string piTerminalName { get; set; }
        [Required(ErrorMessage = "piTerminalIP")]
        public string piTerminalIP { get; set; }
        [Required(ErrorMessage = "piDeviceType")]
        public int piDeviceType { get; set; }


    }
    public class SetPsDoctor
    {
        [Required(ErrorMessage = "请传入UserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "请传入UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请传入Birthday")]
        public int Birthday { get; set; }
        [Required(ErrorMessage = "请输入Gender")]
        public int Gender { get; set; }
        [Required(ErrorMessage = "请输入IDNo")]
        public string IDNo { get; set; }
        [Required(ErrorMessage = "请输入InvalidFlag")]
        public int InvalidFlag { get; set; }
        [Required(ErrorMessage = "piUserId")]
        public string piUserId { get; set; }
        [Required(ErrorMessage = "piTerminalName")]
        public string piTerminalName { get; set; }
        [Required(ErrorMessage = "piTerminalIP")]
        public string piTerminalIP { get; set; }
        [Required(ErrorMessage = "piDeviceType")]
        public int piDeviceType { get; set; }


    }
    public class SetPatBasicInfo
    {
        [Required(ErrorMessage = "请传入UserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "请传入UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请传入Birthday")]
        public int Birthday { get; set; }
        [Required(ErrorMessage = "请输入Gender")]
        public int Gender { get; set; }
        [Required(ErrorMessage = "请输入BloodType")]
        public int BloodType { get; set; }
        [Required(ErrorMessage = "请输入IDNo")]
        public string IDNo { get; set; }
        [Required(ErrorMessage = "请输入DoctorId")]
        public string DoctorId { get; set; }
        [Required(ErrorMessage = "请输入InsuranceType")]
        public string InsuranceType { get; set; }

        [Required(ErrorMessage = "请输入InvalidFlag")]
        public int InvalidFlag { get; set; }
        [Required(ErrorMessage = "piUserId")]
        public string piUserId { get; set; }
        [Required(ErrorMessage = "piTerminalName")]
        public string piTerminalName { get; set; }
        [Required(ErrorMessage = "piTerminalIP")]
        public string piTerminalIP { get; set; }
        [Required(ErrorMessage = "piDeviceType")]
        public int piDeviceType { get; set; }


    }
    public class BasicInfo
    {
        public string UserName { get; set; }
        public string Birthday { get; set; }
        public string IDNo { get; set; }
        public string Gender { get; set; }

    }
    public class PatientBasicInfo
    {
        public string UserName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string IDNo { get; set; }
        public string DoctorId { get; set; }
        public string InsuranceType { get; set; }
        public string Birthday { get; set; }
        public string GenderText { get; set; }
        public string BloodTypeText { get; set; }
        public string InsuranceTypeText { get; set; }


    }
    public class UserBasicInfo
    {
        public string UserName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string IDNo { get; set; }
        public string DoctorId { get; set; }
        public string InsuranceType { get; set; }
        public string InvalidFlag { get; set; }
    }
    public class DoctorInfo
    {
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string IDNo { get; set; }
        public string InvalidFlag { get; set; }
    }
    public class RateTable
    {
        public double PlanRate { get; set; }
        public double ComplianceRate { get; set; }
        public double GoalRate { get; set; }

    }

    public class PatientListTable
    {
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string photoAddress { get; set; }
        public string PlanNo { get; set; }
        public string StartDate { get; set; }
        public double Process { get; set; }
        public string RemainingDays { get; set; }
        public List<string> VitalSign { get; set; }
        public double ComplianceRate { get; set; }
        public string TotalDays { get; set; }
        public string Status { get; set; }

    }

    public class PatBasicInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string InsuranceType { get; set; }
        public string Birthday { get; set; }
        public string GenderText { get; set; }
        public string BloodTypeText { get; set; }
        public string InsuranceTypeText { get; set; }
        public string Module { get; set; }
    }

    public class PatientDetailInfo
    {
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeAddress { get; set; }
        public string Occupation { get; set; }
        public string Nationality { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhoneNumber { get; set; }
        public string PhotoAddress { get; set; }
        public string Module { get; set; }
        public string IDNo { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }

    public class PatientALLBasicInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Birthday { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string IDNo { get; set; }
        public string DoctorId { get; set; }
        public string InsuranceType { get; set; }
        public int InvalidFlag { get; set; }
        public string Module { get; set; }
    }


}