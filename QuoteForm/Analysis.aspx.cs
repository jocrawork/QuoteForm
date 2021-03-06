﻿using QuoteForm.Models;
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
    public partial class Analysis : Page
    {
        IDocumentSession session = HttpContext.Current.GetOwinContext().Get<IDocumentSession>();
        ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        Quote quote;
        string QuoteID;

        protected void Page_Load(object sender, EventArgs e)
        {
            quote = session.Load<Quote>(session.Load<ApplicationUser>(User.Identity.GetUserId()).ActiveQuote);
            QuoteID = quote.Id;

            if (!IsPostBack)
            {
                if(quote.linesHW.Count > 0)
                {
                    repHW.DataSource = quote.linesHW;
                    repHW.DataBind();
                }

                if (quote.linesSW.Count > 0)
                {
                    repSW.DataSource = quote.linesSW;
                    repSW.DataBind();
                }

                if (quote.linesCC.Count > 0)
                {
                    repCC.DataSource = quote.linesCC;
                    repCC.DataBind();
                }

                if (quote.linesInst.Count > 0)
                {
                    repInst.DataSource = quote.linesInst;
                    repInst.DataBind();
                }

                if (quote.linesRec.Count > 0)
                {
                    repRec.DataSource = quote.linesRec;
                    repRec.DataBind();
                }
            }

            //Totals
            GrandTotal.Text = quote.GetGrandTotal().ToString();
            GrandMargin.Text = quote.GetGrandMargin().ToString();
            GrandMarginPercent.Text = Math.Round((quote.GetGrandMargin()/quote.GetGrandTotal())*100,2).ToString();
            
        }
    }
}