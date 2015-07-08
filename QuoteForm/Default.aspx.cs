using AjaxControlToolkit;
using QuoteForm.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Humanizer;
using System.Drawing;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using Spire.Pdf.Grid;

namespace QuoteForm
{   
    public partial class _Default : Page
    {
        IDocumentSession session = QuoteForm.DataDocumentStore.Instance.OpenSession();        
        Quote quote;
        string QuoteID;

        List<LineItem> linesHW = new List<LineItem>();
        List<LineItem> linesAcc = new List<LineItem>();
        List<LineItem> linesSW = new List<LineItem>();
        List<LineItem> linesCC = new List<LineItem>();
        List<LineItem> linesInst = new List<LineItem>();

        protected void Page_Load(Object sender, EventArgs e)
        {
            List<Quote> DBquotes = session.Query<Quote>()
                .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                .ToList();

            foreach (Quote q in DBquotes)
            {
                if (q.IsActive)
                {
                    QuoteID = q.Id;
                    quote = session.Load<Quote>(q.Id);
                    
                }
            }

            foreach (EnumCategories c in Enum.GetValues(typeof(EnumCategories)))
            {
                ListItem item = new ListItem(c.Humanize(), c.ToString());
                HardwareCatDDL.Items.Add(item);
            }
            
            if (QuoteID == null) NewQuote();

            if (!IsPostBack)
            {
                foreach(LineItem line in quote.Lines)
                {
                    switch(line.Product.Category)
                    {
                        case "MediaPlayers":
                            linesHW.Add(line);
                            break;
                        case "IndoorDisplays":
                            linesHW.Add(line);
                            break;
                        case "OutDoorDisplays":
                            linesHW.Add(line);
                            break;
                        case "DataCables":
                            linesAcc.Add(line);
                            break;
                        case "AVCables":
                            linesAcc.Add(line);
                            break;
                        case "ExtendersConverters":
                            linesAcc.Add(line);
                            break;
                        case "Splitters":
                            linesAcc.Add(line);
                            break;
                        case "Speakers":
                            linesAcc.Add(line);
                            break;
                        case "UPSBatteries":
                            linesAcc.Add(line);
                            break;
                        case "InstallAccessories":
                            linesAcc.Add(line);
                            break;
                        case "MountsAccessories":
                            linesAcc.Add(line);
                            break;
                        case "SoftwareVitalcast":
                            linesSW.Add(line);
                            break;
                        case "SoftwareQuickcom":
                            linesSW.Add(line);
                            break;
                        case "SoftwareDashboard":
                            linesSW.Add(line);
                            break;
                        case "ContentCreation":
                            linesCC.Add(line);
                            break;
                        case "HostingServices":
                            linesCC.Add(line);
                            break;
                        case "Installation":
                            linesInst.Add(line);
                            break;
                        case "Warranties":
                            linesSW.Add(line);
                            break;
                    }
                }

                LoadQuote(QuoteID);
                repHW.DataSource = linesHW; //TODO: need to filter to only show appropriate lines (ListItem.Product.Category == HW)
                repHW.DataBind();

                repAcc.DataSource = linesAcc;
                repAcc.DataBind();

                repSW.DataSource = linesSW;
                repSW.DataBind();

                repCC.DataSource = linesCC;
                repCC.DataBind();

                repInst.DataSource = linesInst;
                repInst.DataBind();
            }
        }

        protected void LoadQuote(string ID)
        {
            if (quote.Date == "") quote.Date = DateTime.Today.ToShortDateString();

            Owner.Text       = quote.Owner;
            Date.Text        = quote.Date;
            if (quote.QuoteLength == 0) QuoteLength.Text = "";
            else QuoteLength.Text = quote.QuoteLength.ToString();

            PaymentTermsDDL.SelectedIndex = -1;
            PaymentTermsDDL.SelectedValue = quote.PaymentTerms;

            CustContact.Text    = quote.Customer.Contact;
            CustCompany.Text    = quote.Customer.Company;
            CustAddress1.Text   = quote.Customer.Address1;
            CustAddress2.Text   = quote.Customer.Address2;
            CustCityState.Text  = quote.Customer.CityState;
            CustFax.Text        = quote.Customer.Fax;
            CustPhone.Text      = quote.Customer.Phone;
            CustEmail.Text      = quote.Customer.Email;

            BillContact.Text    = quote.Billing.Contact;
            BillCompany.Text    = quote.Billing.Company;
            BillAddress1.Text   = quote.Billing.Address1;
            BillAddress2.Text   = quote.Billing.Address2;
            BillCityState.Text  = quote.Billing.CityState;
            BillFax.Text        = quote.Billing.Fax;
            BillPhone.Text      = quote.Billing.Phone;
            BillEmail.Text      = quote.Billing.Email;

            ShipContact.Text    = quote.Shipping.Contact;
            ShipCompany.Text    = quote.Shipping.Company;
            ShipAddress1.Text   = quote.Shipping.Address1;
            ShipAddress2.Text   = quote.Shipping.Address2;
            ShipCityState.Text  = quote.Shipping.CityState;
            ShipFax.Text        = quote.Shipping.Fax;
            ShipPhone.Text      = quote.Shipping.Phone;
            ShipEmail.Text      = quote.Shipping.Email;

            SourceDDL.SelectedIndex = -1;
            SourceDDL.SelectedValue = quote.Source;
            SpecificSource.Text     = quote.SpecificSource;
            if (quote.LocationCount == 0) LocationCount.Text = "";
                else LocationCount.Text      = quote.LocationCount.ToString();
            POSProvidor.Text        = quote.POSProvidor;
            InstallDate.Text        = quote.InstallDate;

            BusinessUnitDDL.SelectedIndex = -1;
            BusinessUnitDDL.SelectedValue = quote.BusinessUnit;

            NewLocation.Checked = quote.NewLocation;
            Dealer.Checked      = quote.Dealer;
            TaxExempt.Checked   = quote.TaxExempt;

        }

        protected void SaveQuote(Object source, EventArgs e)
        {
            if (quote == null)
            {
                quote = new Quote();
                session.Store(quote);
            }
            else
            {
                quote.Owner = Owner.Text;
                quote.Date = Date.Text;
                if(QuoteLength.Text != "") quote.QuoteLength = Convert.ToInt32(QuoteLength.Text);
                quote.PaymentTerms = PaymentTermsDDL.SelectedValue;

                quote.Customer = new Address();
                quote.Customer.Contact = CustContact.Text;
                quote.Customer.Company = CustCompany.Text;
                quote.Customer.Address1 = CustAddress1.Text;
                quote.Customer.Address2 = CustAddress2.Text;
                quote.Customer.CityState = CustCityState.Text;
                quote.Customer.Fax = CustFax.Text;
                quote.Customer.Phone = CustPhone.Text;
                quote.Customer.Email = CustEmail.Text;

                quote.Billing = new Address();
                quote.Billing.Contact = BillContact.Text;
                quote.Billing.Company = BillCompany.Text;
                quote.Billing.Address1 = BillAddress1.Text;
                quote.Billing.Address2 = BillAddress2.Text;
                quote.Billing.CityState = BillCityState.Text;
                quote.Billing.Fax = BillFax.Text;
                quote.Billing.Phone = BillPhone.Text;
                quote.Billing.Email = BillEmail.Text;

                quote.Shipping = new Address();
                quote.Shipping.Contact = ShipContact.Text;
                quote.Shipping.Company = ShipCompany.Text;
                quote.Shipping.Address1 = ShipAddress1.Text;
                quote.Shipping.Address2 = ShipAddress2.Text;
                quote.Shipping.CityState = ShipCityState.Text;
                quote.Shipping.Fax = ShipFax.Text;
                quote.Shipping.Phone = ShipPhone.Text;
                quote.Shipping.Email = ShipEmail.Text;

                quote.Source = SourceDDL.SelectedValue;
                quote.SpecificSource = SpecificSource.Text;
                if (LocationCount.Text != "") quote.LocationCount = Convert.ToInt32(LocationCount.Text);
                quote.POSProvidor = POSProvidor.Text;
                quote.InstallDate = InstallDate.Text;
                quote.BusinessUnit = BusinessUnitDDL.SelectedValue;

                quote.NewLocation = NewLocation.Checked;
                quote.Dealer = Dealer.Checked;
                quote.TaxExempt = TaxExempt.Checked;
            }

            session.SaveChanges();
        }

        protected void CopyQuote(Object source, EventArgs e)
        {
            Quote temp = new Quote(quote);
            quote.IsActive = false;

            temp.Lines = quote.Lines;
            temp.IsActive = true;
            session.Store(temp);

            session.SaveChanges();
        }

        protected void NewQuote(Object source, EventArgs e) {NewQuote();} 

        protected void NewQuote()
        {
            if(quote != null) quote.IsActive = false;

            Quote temp = new Quote();
            temp.IsActive = true;
            session.Store(temp);

            session.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }

        protected void PDFQuote(Object source, EventArgs e)
        {//demos: http://www.e-iceblue.com/Tutorials/Spire.PDF/Demos.html //had to add permissions to IIS Express Folder for Network Users

            PdfDocument pdf = new PdfDocument();
            PdfPageBase page = pdf.Pages.Add();
                float pageWidth = page.Canvas.ClientSize.Width;
                float y = 0;

            //formatting helpers
            PdfStringFormat centered = new PdfStringFormat(PdfTextAlignment.Center);
            PdfStringFormat rightAlign = new PdfStringFormat(PdfTextAlignment.Right);
            PdfFont helv24 = new PdfFont(PdfFontFamily.Helvetica, 24f);
            PdfFont helv18 = new PdfFont(PdfFontFamily.Helvetica, 18f);
            PdfFont helv14 = new PdfFont(PdfFontFamily.Helvetica, 14f);
            PdfFont helv12 = new PdfFont(PdfFontFamily.Helvetica, 12f);
            PdfFont helv11 = new PdfFont(PdfFontFamily.Helvetica, 11f);
            PdfBrush black = new PdfSolidBrush(Color.Black);
            SizeF size;

            string brand = "Texas Digital Systems, Inc.";
            string address = "400 Technology Parkway  College Station, TX 77845";
            string contact = "(979) 693-9378 Voice  (979) 764-8650 Fax";
            string title = "Proposal Summary";

            //HEADER
            page.Canvas.DrawString(brand, helv24, new PdfSolidBrush(Color.Black), pageWidth/2, y, centered);
                size = helv24.MeasureString(brand);
                y += size.Height + 1;
            page.Canvas.DrawString(address, helv12, black, pageWidth/2, y, centered);
                size = helv12.MeasureString(address);
                y += size.Height + 1;
            page.Canvas.DrawString(contact, helv12, black, pageWidth / 2, y, centered);
                size = helv12.MeasureString(contact);
                y += size.Height + 1;
            page.Canvas.DrawString("Prepared By: " + quote.Owner, helv12, black, 0, y);
            page.Canvas.DrawString("Date: " + quote.Date, helv12, black, pageWidth, y, rightAlign);
                size = helv12.MeasureString(quote.Owner);
                y += size.Height + 5;
            page.Canvas.DrawString(title, helv18, black, pageWidth / 2, y, centered);
                size = helv18.MeasureString(title);
                y += size.Height + 5;

            //ADDRESS TABLE
            PdfTable addressTable = new PdfTable();
            addressTable.Style.CellPadding = 1;
            addressTable.Style.BorderPen = new PdfPen(PdfBrushes.Black, .5f);

            string[] addressData
                = {
                      ";Customer Address;Billing Address;Shipping Address",
                      "Contact;"+quote.Customer.Contact+";"+quote.Billing.Contact+";"+quote.Shipping.Contact,
                      "Company;"+quote.Customer.Company+";"+quote.Billing.Company+";"+quote.Shipping.Company,
                      "Address1;"+quote.Customer.Address1+";"+quote.Billing.Address1+";"+quote.Shipping.Address1,
                      "Address2;"+quote.Customer.Address2+";"+quote.Billing.Address1+";"+quote.Shipping.Address2,
                      "City/State/Zip;"+quote.Customer.CityState+";"+quote.Billing.CityState+";"+quote.Shipping.CityState,
                      "Phone;"+quote.Customer.Phone+";"+quote.Billing.Phone+";"+quote.Shipping.Phone,
                      "Fax;"+quote.Customer.Fax+";"+quote.Billing.Fax+";"+quote.Shipping.Fax,
                      "Email;"+quote.Customer.Email+";"+quote.Billing.Email+";"+quote.Shipping.Email
                  };

            string[][] addressDataSource = new string[addressData.Length][];
            for (int i = 0; i < addressData.Length; i++)
                addressDataSource[i] = addressData[i].Split(';');

            addressTable.DataSource = addressDataSource;
            float width = page.Canvas.ClientSize.Width - (addressTable.Columns.Count + 1) * addressTable.Style.BorderPen.Width;
            for (int i = 0; i < addressTable.Columns.Count; i++)
            {
                if(i==0)
                    addressTable.Columns[i].Width = width * .12f * width;
                else
                    addressTable.Columns[i].Width = width * .2f * width;
            }
            addressTable.BeginRowLayout += new BeginRowLayoutEventHandler(addressTable_BeginRowLayout);

            PdfLayoutResult addressResult = addressTable.Draw(page, new PointF(0, y));
            y += addressResult.Bounds.Height + 5;

            //QUOTE DETAILS
            PdfTable detailsTable = new PdfTable();
            detailsTable.Style.CellPadding = 1;
            detailsTable.Style.BorderPen = new PdfPen(PdfBrushes.Black, .5f);
            detailsTable.Style.DefaultStyle.Font = helv11;
            detailsTable.Style.DefaultStyle.StringFormat = centered;
            width = page.Canvas.ClientSize.Width - (detailsTable.Columns.Count + 1) * detailsTable.Style.BorderPen.Width;

            string[] detailsData
                = {
                      "Source: ;"+quote.Source+";Source Specific: ;"+quote.SpecificSource+";No. Of Locations: ;"+quote.LocationCount,
                      "POS Provider: ;"+quote.POSProvidor+";Install Date: ;"+quote.InstallDate+";Business Unit: ;"+quote.BusinessUnit,
                      "NewLocation: ;"+quote.NewLocation+";Dealer: ;"+quote.Dealer+";Tax Exempt: ;"+quote.TaxExempt
                  };

            string[][] detailsDataSource = new string[detailsData.Length][];
            for (int i = 0; i < detailsData.Length; i++)
                detailsDataSource[i] = detailsData[i].Split(';');

            detailsTable.DataSource = detailsDataSource;
            for (int i = 0; i < detailsTable.Columns.Count; i++)
            {
                if (i %2 != 0)
                    detailsTable.Columns[i].Width = width * .05f * width;
                else
                    detailsTable.Columns[i].Width = width * .08f * width;
            }

            PdfLayoutResult detailsResult = detailsTable.Draw(page, new PointF(0, y));
            y += detailsResult.Bounds.Height + 5;

            //QUOTE LINES

            
            pdf.SaveToFile("HelloWorld.pdf");
            pdf.Close();
            System.Diagnostics.Process.Start("Helloworld.pdf");


        }

        static void addressTable_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        {

            PdfFont helv12 = new PdfFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Bold);
            PdfFont helv11 = new PdfFont(PdfFontFamily.Helvetica, 11f);
            PdfStringFormat centered = new PdfStringFormat(PdfTextAlignment.Center);
            
            if (args.RowIndex == 0)
            {
                //header
                args.CellStyle.Font = helv12;
                args.CellStyle.StringFormat = centered;
                args.CellStyle.BackgroundBrush = PdfBrushes.GreenYellow;
            }
            else
            {
                args.CellStyle.Font = helv11;
                args.CellStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Left);
                args.CellStyle.BackgroundBrush = PdfBrushes.White;
            }
        }

        protected void HardwareCatDDL_IndexChanged(Object source, EventArgs e)
        {
                HardwareProdDDL.Items.Clear();
                HardwareProdDDL.Items.Add("--Select a Product--");
                HardwareProdDDL.AppendDataBoundItems = true;

                List<Product> prods = session.Query<Product>().Where(x => x.Category == Enum.GetName(typeof(EnumCategories),HardwareCatDDL.SelectedIndex)).ToList();

                foreach (Product p in prods)
                {
                    HardwareProdDDL.Items.Add(new ListItem(p.Name, p.Name));
                }
        }

        protected void HardwareProdDDL_IndexChanged(Object source, EventArgs e)
        {
            List<Product> results = session.Query<Product>().Where(x => (x.Category == HardwareCatDDL.SelectedValue) &&
                                                                        (x.Name == HardwareProdDDL.SelectedValue)).ToList();
            Product p = results[0];

            PartNumber.Text = p.PartNumber.ToString();
            UnitPrice.Text = p.Price.ToString();
            //TODO: add default quantity values to product model & products view
        }

        protected void SaveLineItem(Object source, EventArgs e)
        {
            List<Product> results = session.Query<Product>()
                .Where(x => x.PartNumber == Convert.ToInt32(PartNumber.Text)).ToList();

            LineItem line = new LineItem();
                line.Product       = new Product(results[0]);
                line.Product.Price = Convert.ToInt32(UnitPrice.Text);
                line.Quantity      = Convert.ToInt32(Quantity.Text);

            quote.AddLineItem(line);

            session.SaveChanges();

            Response.Redirect(Request.RawUrl);
        }

        protected void rep_ItemCommand(Object source, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                quote.Lines.RemoveAt(index);
                session.Store(quote);
                session.SaveChanges();

                Response.Redirect(Request.RawUrl);
            }
        }

    }
}