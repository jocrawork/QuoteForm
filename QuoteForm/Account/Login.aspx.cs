using System;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using QuoteForm.Models;
using System.Web.Http;
using System.Threading.Tasks;
using Raven.Client;

namespace QuoteForm.Account
{
    [AllowAnonymous]
    public partial class Login : Page
    {
        [AllowAnonymous]
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        [AllowAnonymous]
        protected void LogIn(object sender, EventArgs e)
        {
            var FailureMessage = "";

            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                var session = Context.GetOwinContext().Get<IDocumentSession>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
   
                var user = manager.FindByEmail(Email.Text);
                
                var result = new SignInStatus();
                
                
                
                if(user.IsEmailConfirmed)
                    result = signinManager.PasswordSignIn(user.UserName, Password.Text, RememberMe.Checked, shouldLockout: false);
                else if(!user.IsEmailConfirmed)
                {
                    FailureMessage = "Check your email now for new account activation!";
                    result = SignInStatus.Failure;

                    string code = manager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                    IdentityMessage msg = new IdentityMessage()
                    {
                        Destination = user.Email,
                        Subject = "QuoteForm Email Confirmation",
                        Body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>."
                    };

                    manager.EmailService.SendAsync(msg);
                }
                

                switch (result)
                {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
                                                        Request.QueryString["ReturnUrl"],
                                                        RememberMe.Checked),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        FailureText.Text = "Invalid login attempt. " + FailureMessage;
                        ErrorMessage.Visible = true;
                        break;
                }
            }
        }
    }
}