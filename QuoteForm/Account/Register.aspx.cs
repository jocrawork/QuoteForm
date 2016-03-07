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
using Raven.Client;

namespace QuoteForm.Account
{
    [AllowAnonymous]
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var session = Context.GetOwinContext().Get<IDocumentSession>();

            //EMAIL CONFIRMATION TURNED OFF UNTIL SERVER ACCESS FIXED
            //var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text, PhoneNumber = PhoneNumber.Text };
            
            var user = new ApplicationUser() { UserName = UserName.Text, Email = Email.Text, IsEmailConfirmed = true, PhoneNumber = PhoneNumber.Text };
            session.Store(user);
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                session.SaveChanges();
                
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                

                IdentityMessage msg = new IdentityMessage()
                    {
                        Destination = user.Email,
                        Subject = "QuoteForm Email Confirmation",
                        Body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>."
                    };

                //EMAIL CONFIRMATION TURNED OFF UNTIL SERVER ACCESS FIXED
                //manager.EmailService.SendAsync(msg);
                
                Response.Redirect("~/");
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