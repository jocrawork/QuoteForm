using QuoteForm.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace QuoteForm
{
    public partial class Quotes : Page
    {
        IDocumentSession session = HttpContext.Current.GetOwinContext().Get<IDocumentSession>();
        ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        List<Quote> quotes;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            quotes = session.Query<Quote>()
                .ToList();

            if (!IsPostBack)
            {
                repQuotes.DataSource = quotes;
                repQuotes.DataBind();
            }
        }

        protected void repQuotes_ItemCommand(Object source, CommandEventArgs e)
        {
   
            if (e.CommandName == "Delete")
            {
                string ID = e.CommandArgument.ToString();

                session.Delete(ID);
                session.SaveChanges();

                Response.Redirect(Request.RawUrl);
            }
            
            if(e.CommandName == "Choose")
            {
                foreach(Quote q in quotes)
                {
                    if(q.Customer.Contact == "") //removes blank (new) quotes from DB when choosing another. should never be more than 1/user due to validation on Customer.Contact
                        session.Delete(q);

                    if (q.Id == e.CommandArgument.ToString())
                        session.Load<ApplicationUser>(User.Identity.GetUserId()).ActiveQuote = q.Id;
                }

                session.SaveChanges();

                Response.Redirect("~/");
            }

            if(e.CommandName == "Email")
            {
                var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = userManager.FindByName(e.CommandArgument.ToString());

                Response.Redirect( "mailto:" + user.Email );
            }

            

        }

        public string QuoteClass(string user)
        {
            return (user == User.Identity.Name) ? "myquote" : "otherquote";
        }

        public string GetGrandTotalString(Quote q)
        {
            return q.GetGrandTotal().ToString();
        }
    }
}