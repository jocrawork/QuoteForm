using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;

namespace QuoteForm
{
    public partial class HardwareDisplays : System.Web.UI.Page
    {
        IDocumentSession session = HttpContext.Current.GetOwinContext().Get<IDocumentSession>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}