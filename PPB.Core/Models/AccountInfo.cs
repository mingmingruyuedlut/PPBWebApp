using PPB.Constant.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    #region Login
    public class LoginInfo
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class LoginUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserLevelType RoleType { get; set; }
        public LoginUserValidateResult ValidateResult { get; set; }
    }

    public class LoginUserValidateResult
    {
        public LoginUserValidateResultType ValidateResultType { get; set; }
        public string Message { get; set; }
    }

    public enum LoginUserValidateResultType
    {
        Success,
        UserNotExist,
        PasswordInvalid
    }
    #endregion


    #region Register
    public class RegisterInfo
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmed")]
        public string ConfirmedNewPassword { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }

        public RegisterValidateResult ValidateResult { get; set; }
    }

    public class RegisterValidateResult
    {
        public RegisterValidateResultType ValidateResultType { get; set; }
        public string Message { get; set; }
    }

    public enum RegisterValidateResultType
    {
        Success,
        UserIsExist,
        InvalidPassword
    }
    #endregion
}
