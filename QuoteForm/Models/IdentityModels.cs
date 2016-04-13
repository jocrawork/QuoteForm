using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using RavenDB.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using QuoteForm.Models;
using Raven.Client;

using System.Linq;
using Raven.Client.Linq;
using System.Collections.Generic;

namespace QuoteForm.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string ActiveQuote { get; set; }
        
        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }
    }

    public class RavenRole 
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RavenRole(string name)
        {
            this.Name = name;
        }
    }

    public class RavenRoleStore
    {
        protected DataDocumentStore context { get; set; }
        protected IDocumentSession session { get; set; }

        public RavenRoleStore()
        {
            session = QuoteForm.DataDocumentStore.Instance.OpenSession();
        }

        public void CreateRole(string name)
        {
            if (RoleExists(name)) return;
            var role = new RavenRole(name);

            session.Store(role);
            session.SaveChanges();
        }

        public List<ApplicationUser> GetUsersInRole(string role)
        {
            return session.Query<ApplicationUser>()
                .Where(x => x.Roles.Any(r => r == role))
                .ToList();
        }

        public List<RavenRole> GetAllRoles()
        {
            List<RavenRole> roles = session.Query<RavenRole>().ToList();
            return roles;
        }

        public bool RoleExists(string name)
        {
            return session.Query<RavenRole>().Where(r => r.Name == name).Any();
        }
    }
}

#region Helpers
namespace QuoteForm
{
    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("~/");
            }
        }
    }
}
#endregion
