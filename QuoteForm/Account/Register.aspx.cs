using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using QuoteForm.Models;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace QuoteForm.Account
{
    [AllowAnonymous]
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

                var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text, PhoneNumber = PhoneNumber.Text };
                IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                IdentityMessage msg = new IdentityMessage()
                    {
                        Destination = user.Email,
                        Subject = "QuoteForm Email Confirmation",
                        Body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>."
                    };

                manager.EmailService.SendAsync(msg);

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void EmailValidate(Object source, ServerValidateEventArgs e)
        {//internal emails only
            if (Email.Text.EndsWith("@ncr.com"))
                e.IsValid=true;
            else e.IsValid=false;
        }
    }
}