using AjaxControlToolkit;
using QuoteForm.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Humanizer;
using System.Drawing;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using Spire.Pdf.Grid;
using System.Web.Http;
using Telerik.Web.UI;
using System.Collections;

namespace QuoteForm
{   
    public partial class _Default : Page
    {
        IDocumentSession session = HttpContext.Current.GetOwinContext().Get<IDocumentSession>();
        ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        Quote quote;
        string QuoteID;

        protected void Page_Load(Object sender, EventArgs e)
        {

            //var active = manager.FindById(userID).ActiveQuote;
               //this usermanager is crap ever since I went to users in RavenDB

            var user = session.Load<ApplicationUser>(User.Identity.GetUserId());

            //load active quote for current user
            if (user.ActiveQuote != null)
            {
                quote = session.Load<Quote>(user.ActiveQuote);
                QuoteID = quote.Id;
            }
            else NewQuote();
                                    
            if (!IsPostBack)
            {
                LoadQuote(QuoteID);
                repHW.DataSource = quote.linesHW;
                repHW.DataBind();

                repSW.DataSource = quote.linesSW;
                repSW.DataBind();

                repCC.DataSource = quote.linesCC;
                repCC.DataBind();

                repInst.DataSource = quote.linesInst;
                repInst.DataBind();

                repRec.DataSource = quote.linesRec;
                repRec.DataBind();
            }

            //ScriptManager.RegisterAsyncPostBackControl();
        }

        protected void LoadQuote(string ID)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            
            Owner.Text = quote.Owner;
            if (Owner.Text == "") Owner.Text = userManager.FindById(User.Identity.GetUserId()).UserName.ToString();
            PhoneNumber.Text = quote.PhoneNumber;
            if (PhoneNumber.Text == "") PhoneNumber.Text = userManager.FindById(User.Identity.GetUserId()).PhoneNumber.ToString();
            Date.Text = quote.Date;
            if (Date.Text == "") Date.Text = DateTime.Today.ToShortDateString();
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

            SourceDDL.SelectedValue = quote.Source;
            SpecificSource.Text     = quote.SpecificSource;
            if (quote.LocationCount == 0) LocationCount.Text = "";
                else LocationCount.Text      = quote.LocationCount.ToString();
            POSProvidor.Text        = quote.POSProvidor;
            InstallDate.Text        = quote.InstallDate;
            BusinessUnitDDL.SelectedValue = quote.BusinessUnit;

            NewLocation.Checked = quote.NewLocation;
            Dealer.Checked      = quote.Dealer;
            TaxStatusDDL.SelectedValue = quote.TaxExempt;
            

            //TOTALS SECTION
            HWtotalCell.Text    = quote.TotalLines("Hardware").ToString();
            SWtotalCell.Text    = quote.TotalLines("Software").ToString();
            CCtotalCell.Text    = quote.TotalLines("ContentCreation").ToString();
            INSTtotalCell.Text  = quote.TotalLines("Installation").ToString();
            RECtotalCell.Text   = quote.TotalLines("Recurring").ToString();
            TotalCell.Text      = quote.GetGrandTotal().ToString();

            if (quote.Freight == 0) Freight.Text = "";
                else Freight.Text = quote.Freight.ToString();
            if (quote.SalesTax == 0) SalesTax.Text = "";
                else SalesTax.Text = quote.SalesTax.ToString();

            InternalNotes.InnerText = quote.InternalNotes;
            ExternalNotes.InnerText = quote.ExternalNotes;

        }
        protected void SaveQuote(Object source, EventArgs e) { SaveQuote();}     
            protected void SaveQuote()
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
                    quote.PhoneNumber = PhoneNumber.Text;
                    quote.Email = manager.FindById(User.Identity.GetUserId()).Email.ToString();
                    if (QuoteLength.Text != "") quote.QuoteLength = Convert.ToInt32(QuoteLength.Text);
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

                    quote.TaxExempt = TaxStatusDDL.SelectedValue;

                    if (Freight.Text != "") quote.Freight = Convert.ToDouble(Freight.Text);
                        else quote.Freight = 0;
                    if (SalesTax.Text != "") quote.SalesTax = Convert.ToDouble(SalesTax.Text);
                    else quote.SalesTax = 0;

                    quote.InternalNotes = InternalNotes.InnerText;
                    quote.ExternalNotes = ExternalNotes.InnerText;
                }

                session.SaveChanges();
            }
        protected void CopyQuote(Object source, EventArgs e)
        {
            Quote temp = new Quote(quote);

            var user = session.Load<ApplicationUser>(User.Identity.GetUserId());
                user.ActiveQuote = temp.Id;

            session.Store(temp);

            session.SaveChanges();
        }
        protected void NewQuote(Object source, EventArgs e) {NewQuote();} 
            protected void NewQuote()
        {
            var user = session.Load<ApplicationUser>(User.Identity.GetUserId());
                
                quote = new Quote();
            
            session.Store(quote);

            user.ActiveQuote = quote.Id;
            QuoteID = quote.Id;


            session.SaveChanges();
            Response.Redirect(Request.RawUrl);
        }
        protected void PDFQuote(Object source, EventArgs e)
        {//demos: http://www.e-iceblue.com/Tutorials/Spire.PDF/Demos.html //had to add permissions to IIS Express Folder for Network Users. Wont export more than 10 pages on free version.

            SaveQuote();

            var filename = PdfFileName.Value;
            if (filename == "CANCELDONOTMAKE") return;

            PdfDocument pdf = new PdfDocument();
            PdfPageBase page = pdf.Pages.Add();
                float pageWidth = page.Canvas.ClientSize.Width;
                float y = 0;

            //formatting helpers
            PdfStringFormat centered = new PdfStringFormat(PdfTextAlignment.Center);
            PdfStringFormat rightAlign = new PdfStringFormat(PdfTextAlignment.Right);
            PdfFont helv24 = new PdfFont(PdfFontFamily.Helvetica, 24f, PdfFontStyle.Bold);
            PdfFont helv20 = new PdfFont(PdfFontFamily.Helvetica, 20f, PdfFontStyle.Bold);
            PdfFont helv16 = new PdfFont(PdfFontFamily.Helvetica, 16f, PdfFontStyle.Bold);
            PdfFont helv14 = new PdfFont(PdfFontFamily.Helvetica, 14f);
            PdfFont helv12 = new PdfFont(PdfFontFamily.Helvetica, 12f);
            PdfFont helv12Bold = new PdfFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Bold);
            PdfFont helv11 = new PdfFont(PdfFontFamily.Helvetica, 11f);
            PdfFont helv9Ital = new PdfFont(PdfFontFamily.Helvetica, 9f, PdfFontStyle.Italic);
            PdfFont helv8  = new PdfFont(PdfFontFamily.Helvetica, 8f);
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
            page.Canvas.DrawString("By: " + quote.Owner, helv12, black, 0, y);
            page.Canvas.DrawString(quote.Email, helv12, black, pageWidth, y, rightAlign);
            size = helv12.MeasureString(contact);
            y += size.Height + 1;
            page.Canvas.DrawString("Date: " + quote.Date, helv12, black, 0, y);
            page.Canvas.DrawString("PN: " + quote.PhoneNumber, helv12, black, pageWidth, y, rightAlign);
                size = helv12.MeasureString(quote.Owner);
                y += size.Height + 5;
            page.Canvas.DrawString(title, helv20, black, pageWidth / 2, y, centered);
                size = helv20.MeasureString(title);
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
                      "Address2;"+quote.Customer.Address2+";"+quote.Billing.Address2+";"+quote.Shipping.Address2,
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

            //converting t/f to y/n
            string NewLocation, Dealer, TaxExempt;
            if (quote.NewLocation) NewLocation = "Yes";
                else NewLocation = "No";
            if (quote.Dealer) Dealer = "Yes";
                else Dealer = "No";
            if (quote.TaxExempt == "exempt") TaxExempt = "Yes";
                else TaxExempt = "No";

            string[] detailsData
                = {
                      "Source: ;"+quote.Source+";Source Specific: ;"+quote.SpecificSource+";No. Of Locations: ;"+quote.LocationCount,
                      "POS Provider: ;"+quote.POSProvidor+";Install Date: ;"+quote.InstallDate+";Business Unit: ;"+quote.BusinessUnit,
                      "New Location: ;"+NewLocation+";Dealer: ;"+Dealer+";Tax Exempt: ;"+TaxExempt
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
            if(quote.linesHW.Count > 0)
            {
                page.Canvas.DrawString("Hardware", helv16, black, pageWidth / 2, y, centered);
                    size = helv14.MeasureString("Hardware");
                    y += size.Height + 2;
                y += (buildPdfLines(page, quote.linesHW,"Hardware", y)).Bounds.Height + 2;
            }
            
            if(quote.linesSW.Count > 0)
            {
                page.Canvas.DrawString("Software", helv16, black, pageWidth / 2, y, centered);
                    size = helv16.MeasureString("Software & Maintenance");
                    y += size.Height + 2;
                y += (buildPdfLines(page, quote.linesSW,"Software", y)).Bounds.Height + 2;
            }

            if(quote.linesCC.Count > 0)
            {
                page.Canvas.DrawString("Content", helv16, black, pageWidth / 2, y, centered);
                    size = helv16.MeasureString("Content");
                    y += size.Height + 2;
                y += (buildPdfLines(page, quote.linesCC,"Content", y)).Bounds.Height + 2;
            }
            
            if(quote.linesInst.Count > 0)
            {
                page.Canvas.DrawString("Installation", helv16, black, pageWidth / 2, y, centered);
                    size = helv16.MeasureString("Installation");
                    y += size.Height + 2;
                y += (buildPdfLines(page, quote.linesInst, "Installation", y)).Bounds.Height + 2;
            }

            if (quote.linesRec.Count > 0)
            {
                page.Canvas.DrawString("Recurring", helv16, black, pageWidth / 2, y, centered);
                size = helv16.MeasureString("Recurring");
                y += size.Height + 2;
                y += (buildPdfLines(page, quote.linesRec, "Recurring", y)).Bounds.Height + 2;
            }

            bool FreightExists = false; if (quote.Freight > 0) FreightExists = true;
            bool SalesTaxExists = false; if (quote.SalesTax > 0) SalesTaxExists = true;
            double GrandTotal = quote.GetGrandTotal();

            //NOTES
            if (quote.ExternalNotes.Length > 0)
            {
                string notes = quote.ExternalNotes;
                PdfStringLayouter layouter = new PdfStringLayouter();
                PdfStringFormat format = new PdfStringFormat();
                format.LineSpacing = helv11.Size * 1.5f;

                PdfStringLayoutResult result = layouter.Layout(notes, helv11, format, new SizeF(pageWidth, y));

                page.Canvas.DrawString("Notes", helv14, black, pageWidth / 2, y, centered);
                size = helv14.MeasureString("LULZ");
                y += size.Height + 2;


                foreach (LineInfo line in result.Lines)
                {
                    page.Canvas.DrawString(line.Text, helv11, black, 0, y, format);
                    y = y + result.LineHeight;
                }

            }

            y += 5;
            page.Canvas.DrawLine(new PdfPen(PdfBrushes.Black, .5f), new PointF(0, y), new PointF(pageWidth, y));
            y += 5;
             
   
            //TOTALS
            if(FreightExists || SalesTaxExists)
            {
                page.Canvas.DrawString("Subtotal: $" + GrandTotal.ToString(), helv12, black, 0, y);
            }
            if(FreightExists)
            {
                page.Canvas.DrawString("Freight: $" + quote.Freight.ToString(), helv12, black, pageWidth/4, y);
                GrandTotal += quote.Freight;
            }
            if (SalesTaxExists)
            {
                page.Canvas.DrawString("Sales Tax: $" + quote.SalesTax.ToString(), helv12, black, pageWidth / 2, y);
                GrandTotal += quote.SalesTax;
            }

            page.Canvas.DrawString("Total: $" + GrandTotal.ToString(), helv12Bold, black, pageWidth, y, rightAlign);
                size = helv12Bold.MeasureString("999999");

            y += size.Height + 5;
            page.Canvas.DrawLine(new PdfPen(PdfBrushes.Black, .5f), new PointF(0, y), new PointF(pageWidth, y));
            y += 5;

            //FINE PRINT
            page.Canvas.DrawString("Quote is good for: " + quote.QuoteLength + " days", helv8, black, 0, y);
            page.Canvas.DrawString("F.O.B. College Station, TX", helv8, black, pageWidth / 2, y, centered);
            page.Canvas.DrawString("Payment Terms: " + quote.PaymentTerms, helv8, black, pageWidth, y, rightAlign);
                size = helv8.MeasureString("THESE WORDS DON'T MATTER");
                y += size.Height + 1;

            page.Canvas.DrawString("This is not an invoice and may not include freight and/or sales tax. An invoice will be sent upon receipt of the signed quote.", helv9Ital, black, pageWidth/2, y, centered);
                size = helv9Ital.MeasureString("ONLY DEVS WILL SEE THIS");
                y += size.Height + 10;

            page.Canvas.DrawString("Please sign to accept this quotation: ", helv8, black, 0, y);
                size = helv8.MeasureString("I CAN SAY WHATEVER I WANT");
                page.Canvas.DrawLine(new PdfPen(PdfBrushes.Black, .5f), new PointF(150, y+size.Height), new PointF(350, y+size.Height));
                y += size.Height + 1;

            page.Canvas.DrawString("By signing I agree that I have read, understand and agree to be bound by the Texas Digital Standard Terms and Conditions Applicable to", helv8, black, 0, y);
                size = helv8.MeasureString("PAY UP GUY");
                y += size.Height + 1;
            
            page.Canvas.DrawString("Quotes and Purchase Orders accessible at: ", helv8, black, 0, y);
                size = helv8.MeasureString("Quotes and Purchase Orders accessible at: ");
                page.Canvas.DrawString("http://www.ncr.com/wp-content/uploads/TXDigital_Terms_and_Conditions.pdf", helv8, PdfBrushes.DarkGreen, size.Width, y);
                y += size.Height + 1;

            page.Canvas.DrawString("After signing please fax to (979) 764-8650", helv8, black, 0, y);
            page.Canvas.DrawString("Delivery ARO: 45-60 days", helv8, black, pageWidth, y, rightAlign);
                size = helv8.MeasureString("THIS ISNT THE END LOL");
                y += size.Height + 1;

            
            //pdf.SaveToFile(filename);
            pdf.SaveToHttpResponse(filename, Response, HttpReadType.Save);
            pdf.Close();
            System.Diagnostics.Process.Start(filename);


        }

        protected PdfLayoutResult buildPdfLines(PdfPageBase page, List<LineItem> list, string category, float y)
        {

            PdfFont helv14 = new PdfFont(PdfFontFamily.Helvetica, 14f);
            PdfFont helv12 = new PdfFont(PdfFontFamily.Helvetica, 12f);
            PdfFont helv11 = new PdfFont(PdfFontFamily.Helvetica, 11f);

            PdfTable LinesTable = new PdfTable();
            LinesTable.Style.CellPadding = 1;
            LinesTable.Style.DefaultStyle.Font = helv11;
            
            List<string> data = new List<string>();
            double subtotal = 0;

            if(category == "Recurring")
                data.Add("Product;Part Number;Monthly Rate;Quantity;Price");
            else
                data.Add("Product;Part Number;Unit Price;Quantity;Price");
            foreach (LineItem line in list)
            {
                data.Add(line.Product.Name + ";" + line.Product.PartNumber + ";$" + line.Product.Price + ";" + line.Quantity + ";$" + line.Total);
                subtotal += line.Total;
            } data.Add(";;; Subtotal: ;$" + subtotal.ToString());


            string[][] dataSource = new string[data.Count][];
            for (int i = 0; i < data.Count; i++)
                dataSource[i] = data[i].Split(';');

            LinesTable.DataSource = dataSource;

            LinesTable.BeginRowLayout += new BeginRowLayoutEventHandler(LinesTable_BeginRowLayout);

            LinesTable.Columns[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            LinesTable.Columns[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            LinesTable.Columns[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Right);
            LinesTable.Columns[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Right);


            float width = page.Canvas.ClientSize.Width;
            for (int i = 0; i < LinesTable.Columns.Count; i++)
            {
                if (i == 0)
                    LinesTable.Columns[i].Width = width * .1f * width;
                else
                    LinesTable.Columns[i].Width = width * .045f * width;
            }

            return LinesTable.Draw(page, new PointF(0, y));
            
        }
        static void LinesTable_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        {
            PdfFont helv11 = new PdfFont(PdfFontFamily.Helvetica, 11f);
            PdfFont helv12Bold = new PdfFont(PdfFontFamily.Helvetica, 12f, PdfFontStyle.Bold);
            PdfStringFormat centered = new PdfStringFormat(PdfTextAlignment.Center);
            PdfBrush gray = new PdfSolidBrush(Color.LightGray);
            PdfBrush clear = new PdfSolidBrush(Color.Transparent);

            args.CellStyle.BorderPen = new PdfPen(Color.Transparent);
            args.CellStyle.BackgroundBrush = clear;


            if(args.RowIndex == 0)
            {
                //header
                args.CellStyle.Font = helv12Bold;

            }
            else 
                args.CellStyle.Font = helv11;
                
            if(args.RowIndex % 2 != 0)
            {
                args.CellStyle.BackgroundBrush = gray;
            }
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
                args.CellStyle.BackgroundBrush = PdfBrushes.LightGray;
            }
            else
            {
                args.CellStyle.Font = helv11;
                args.CellStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Left);
                args.CellStyle.BackgroundBrush = PdfBrushes.White;
            }
        }
        
        protected void ProductSelected(Object source, EventArgs e) //serverside product load
        {
            RadComboBox temp = (RadComboBox)source;
            Product p;

            if (temp.SelectedIndex >= 0)
            {
                p = session.Query<Product>()
                    .Where(x => x.Name == temp.SelectedItem.Text)
                    .FirstOrDefault();

                var repParent = temp.Parent;
                //var repParent = ((UpdatePanel)temp.NamingContainer.FindControl("UpdateHardwareLine")).ContentTemplateContainer;

                ((TextBox)repParent.FindControl("AddPartNumber")).Text = p.PartNumber;
                ((TextBox)repParent.FindControl("AddPartCost")).Text = p.Cost.ToString();
                ((TextBox)repParent.FindControl("AddUnitPrice")).Text = p.Price.ToString();
                ((TextBox)repParent.FindControl("AddQuantity")).Text = p.DefaultQuantity.ToString();
            }
        }

        protected void TaxStatusValidation(Object source, ServerValidateEventArgs e)
        {
            if(TaxStatusDDL.SelectedValue == null)
            {
                e.IsValid = false;
                return;
            }
            else
            {
                e.IsValid = true;
                return;
            }
        }

        protected void rep_ItemCommand(Object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string category = ((HiddenField)e.Item.FindControl("Category")).Value.ToString();

                quote.RemoveLineItem(category,index);
                session.Store(quote);
                session.SaveChanges();

                Response.Redirect(Request.RawUrl);
            }
            if(e.CommandName == "Add")
            {

                if (true) //(EmptyFieldCheck(e))
                {
                    LineItem line = new LineItem();

                    line.Product.Category = ((HiddenField)e.Item.FindControl("AddCategory")).Value.ToString();
                    line.Product.Name = ((RadComboBox)e.Item.FindControl("AddProduct")).SelectedItem.Text;
                    line.Product.PartNumber = ((TextBox)e.Item.FindControl("AddPartNumber")).Text;
                    line.Product.Cost = Convert.ToDouble(((TextBox)e.Item.FindControl("AddPartCost")).Text);
                    line.Product.Price = Convert.ToDouble(((TextBox)e.Item.FindControl("AddUnitPrice")).Text);
                    line.Quantity = Convert.ToInt32(((TextBox)e.Item.FindControl("AddQuantity")).Text);
                    line.Total = line.Product.Price * line.Quantity;

                    quote.AddLineItem(line);
                    session.SaveChanges();
                    Response.Redirect(Request.RawUrl);
                }
                
               
            }
        }

        protected void LoadProductsByCategory(Object source, EventArgs e)
        {
            RadComboBox temp = (RadComboBox)source;
            string category = ((HiddenField)temp.NamingContainer.FindControl("AddCategory")).Value.ToString();

            List<Product> prods = session.Query<Product>()
                .Where(x => x.Category == category)
                .OrderBy(x => x.Name)
                .Take(int.MaxValue)
                .ToList();

            temp.DefaultItem.Text = "Select a Product";
            temp.DefaultItem.Value = "-1";

            foreach (Product p in prods)
                temp.Items.Add(new RadComboBoxItem(p.Name));
        }
    }
}