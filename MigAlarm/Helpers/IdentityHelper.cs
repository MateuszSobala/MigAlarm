using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using MigAlarm.Models;

namespace MigAlarm.Helpers
{
    public class IdentityHelper
    {
        private static readonly MigAlarmContext Db = new MigAlarmContext();

        public static User User
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // The user is authenticated. Return the user from the forms auth ticket.
                    return Db.Users.First(x => x.UserId == ((AppPrincipal)HttpContext.Current.User).User.UserId);
                }
                if (HttpContext.Current.Items.Contains("User"))
                {
                    // The user is not authenticated, but has successfully logged in.
                    return (User)HttpContext.Current.Items["User"];
                }
                return null;
            }
        }

        public static User AuthenticateUser(string email, string password)
        {
            var user = Db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            return user;
        }

        public static bool ValidateUser(LoginViewModel logon, HttpResponseBase response)
        {
            if (!Membership.ValidateUser(logon.Email, logon.Password)) return false;

            Db.Configuration.LazyLoadingEnabled = false;
            Db.Configuration.ProxyCreationEnabled = false;

            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(User);

            Db.Configuration.LazyLoadingEnabled = true;
            Db.Configuration.ProxyCreationEnabled = true;

            var ticket = new FormsAuthenticationTicket(1,
                logon.Email,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                true,
                userData,
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            return true;
        }

        public static void Logoff(HttpSessionStateBase session, HttpResponseBase response)
        {
            // Delete the user details from cache.
            session.Abandon();

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            // Clear authentication cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "") {Expires = DateTime.Now.AddYears(-1)};
            response.Cookies.Add(cookie);
        }
    }

    public class AppMembershipProvider : MembershipProvider
    {
        public override bool ValidateUser(string username, string password)
        {
            var user = IdentityHelper.AuthenticateUser(username, password);
            if (user == null) return false;
            HttpContext.Current.Items.Add("User", user);

            return true;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = IdentityHelper.User;
            return user != null ? new MembershipUser("IdentityHelper", username, user.UserId, user.Email, null, null, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue) : null;
        }

        #region Not Used

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
            /*string[] parts = providerUserKey.ToString().Split(new char[] { '_' });
            if (parts.Length == 2)
            {
                string username = parts[0];
                long userId;
                if (Int64.TryParse(parts[1], out userId))
                {
                    User user = UserClient.Load(username, userId);
                    if (user.UserDataUserId > 0)
                    {
                        return new MembershipUser("APMembershipProvider", user.UserDataLoginName, UserManager.User.UserDataUserId, UserManager.User.UserDataLoginName, null, null, true, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                    }
                }
            }
            return null;*/
        }

        #endregion
    }

    public class AppPrincipal : IPrincipal
    {
        public AppPrincipal(IIdentity identity)
        {
            Identity = identity;
        }

        public IIdentity Identity { get; }

        public User User { get; set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}