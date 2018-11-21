using PPB.DBManager.Models;
using PPB.Constant.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class AccountManager
    {
        /// <summary>
        /// Validate account sign in
        /// </summary>
        /// <param name="username">it's login name</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public LoginUser AccountSignInValidation(string username, string password)
        {
            var currentUser = new LoginUser();

            //to-do, check user info from db
            if (!string.IsNullOrWhiteSpace(username) && username.Equals("Administrator"))
            {
                if (password.Equals("Administrator"))
                {
                    currentUser.UserName = username;
                    currentUser.RoleType = UserLevelType.Administrator;
                    currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.Success, Message = "Login successful." };
                }
                else
                {
                    currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.PasswordInvalid, Message = "Password is invalid." };
                }
            }
            else
            {
                currentUser.ValidateResult = new LoginUserValidateResult() { ValidateResultType = LoginUserValidateResultType.UserNotExist, Message = "User is not exist." };
            }
            
            return currentUser;
        }
    }
}
