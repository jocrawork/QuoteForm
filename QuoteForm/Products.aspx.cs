using QuoteForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Raven.Client;
using Humanizer;
using Microsoft.AspNet.Identity.Owin;

namespace QuoteForm
{
    public partial class Products : Page
    {
        IDocumentSession session = HttpContext.Current.GetOwinContext().Get<IDocumentSession>();
        Quote quote = new Quote();

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Product> prods = session.Query<Product>()
                .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                .Take(int.MaxValue)     //by default, RavenDB only allows 128 items per request. idk why, they stoops
                .ToList();

            if (!IsPostBack)
            {
                foreach (EnumCategories c in Enum.GetValues(typeof(EnumCategories)))
                {
                    ListItem item = new ListItem(c.Humanize(), c.ToString());
                    CategoryDDL.Items.Add(item);
                }

                foreach (Product p in prods)
                    //p.Category = ((EnumCategories)Enum.Parse(typeof(EnumCategories), p.Category)).Humanize();
                   
                repProducts.DataSource = prods;
                repProducts.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            var ID = HiddenFieldAdd.Value;
            Product p = new Product();

            //CHECKS DB for existing product to update
            if(ID != "") p = session.Load<Product>(ID);
            
            //prevents accidental override
            if(p.PartNumber != PartNumber.Text)
                p = new Product();

            p.Category = CategoryDDL.SelectedValue;
            p.Name = Name.Text;
            p.PartNumber = PartNumber.Text;
            if (Price.Text != "") p.Price = Convert.ToDouble(Price.Text);
            p.Cost = Convert.ToDouble(Cost.Text);
            p.DefaultQuantity = Convert.ToInt32(DefaultQuantity.Text);
            
            session.Store(p);
            session.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }

        protected void repProducts_ItemCommand(object source, CommandEventArgs e)
        {
            if(e.CommandName == "Edit")
            {
                string ID = e.CommandArgument.ToString();
                HiddenFieldAdd.Value = ID;

                var prod = session.Load<Product>(ID);
                CategoryDDL.SelectedValue = prod.Category;
                Name.Text = prod.Name;
                PartNumber.Text = prod.PartNumber;
                Price.Text = prod.Price.ToString();
                Cost.Text = prod.Cost.ToString();
                DefaultQuantity.Text = prod.DefaultQuantity.ToString();


            }
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
