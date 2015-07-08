using QuoteForm.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuoteForm
{
    public partial class Quotes : Page
    {
        IDocumentSession session = QuoteForm.DataDocumentStore.Instance.OpenSession();
        List<Quote> quotes;

        protected void Page_Load(object sender, EventArgs e)
        {
            quotes = session.Query<Quote>().ToList();

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
                    q.IsActive = false;
                    if (q.Id == e.CommandArgument.ToString()) q.IsActive = true;
                }

                session.SaveChanges();

                Response.Redirect("~/");
            }

            

        }

    }
}