using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuoteForm
{
    public partial class HardwareDisplays : System.Web.UI.Page
    {
        IDocumentSession session = QuoteForm.DataDocumentStore.Instance.OpenSession();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}