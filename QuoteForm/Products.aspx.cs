using QuoteForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Raven.Client;
using Humanizer;

namespace QuoteForm
{
    public partial class Products : Page
    {
        IDocumentSession session = QuoteForm.DataDocumentStore.Instance.OpenSession();
        Quote quote = new Quote();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Product> prods = session.Query<Product>()
                .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                .ToList();

            if (!IsPostBack)
            {
                foreach (EnumCategories c in Enum.GetValues(typeof(EnumCategories)))
                {
                    ListItem item = new ListItem(c.Humanize(), c.ToString());
                    CategoryDDL.Items.Add(item);
                }

                foreach (Product p in prods)
                    p.Category = ((EnumCategories)Enum.Parse(typeof(EnumCategories), p.Category)).Humanize();
                   
                repProducts.DataSource = prods;
                repProducts.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Category = CategoryDDL.SelectedValue;
            p.Name = Name.Text;
            p.PartNumber = Convert.ToInt32(PartNumber.Text);
            if (Price.Text != "") p.Price = Convert.ToDouble(Price.Text);
            p.Cost = Convert.ToDouble(Cost.Text);

            session.Store(p);
            session.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }

        protected void repProducts_ItemCommand(object source, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string ID = e.CommandArgument.ToString();

                session.Delete(ID);
                session.SaveChanges();

                Response.Redirect(Request.RawUrl);
            }
        }

    }
}
