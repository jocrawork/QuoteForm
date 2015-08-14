using QuoteForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuoteForm
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var users = context.Users.ToList();

            repUsers.DataSource = users;
            repUsers.DataBind();
        }
    }
}