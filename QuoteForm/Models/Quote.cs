using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteForm.Models
{
    public class LineItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }

        public LineItem()
        {
            Product = new Product();
        }
        
    }

    public class Address
    {
        public string Contact { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string CityState { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Address()
        {
            Contact = "";
            Company = "";
            Address1 = "";
            Address2 = "";
            CityState = "";
            Fax = "";
            Phone = "";
            Email = "";
        }
    }
    
    public class Quote
    {
        //subsections
        public List<LineItem> linesHW = new List<LineItem>();
        public List<LineItem> linesAcc = new List<LineItem>();
        public List<LineItem> linesSW = new List<LineItem>();
        public List<LineItem> linesCC = new List<LineItem>();
        public List<LineItem> linesInst = new List<LineItem>();
        public List<LineItem> linesRec = new List<LineItem>();

        public string Id { get; set; }
        public string Owner { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; }

        public int QuoteLength { get; set; }
        public string PaymentTerms { get; set; } 

        public Address Customer { get; set; }
        public Address Billing { get; set; }
        public Address Shipping { get; set; }

        public string Source { get; set; } 
        public string SpecificSource { get; set; }
        public int LocationCount { get; set; }
        public string POSProvidor { get; set; }
        public string InstallDate { get; set; }
        public string BusinessUnit { get; set; } 

        public bool NewLocation { get; set; }
        public bool Dealer { get; set; }
        public string TaxExempt { get; set; }

        public double Freight { get; set; }
        public double SalesTax { get; set; }

        public string InternalNotes { get; set; }
        public string ExternalNotes { get; set; }

        public Quote()
        {
            TaxExempt = null;
            IsActive = false;

            Customer = new Address();
            Billing  = new Address();
            Shipping = new Address();
        }

        public Quote(Quote q)
        {
            this.Id = null;
            this.Owner = q.Owner;
            this.Date = q.Date;

            this.QuoteLength = q.QuoteLength;
            this.PaymentTerms = q.PaymentTerms;

            this.Customer = q.Customer;
            this.Billing = q.Billing;
            this.Shipping = q.Shipping;

            this.Source = q.Source;
            this.SpecificSource = q.SpecificSource;
            this.LocationCount = q.LocationCount;
            this.POSProvidor = q.POSProvidor;
            this.InstallDate = q.InstallDate;
            this.BusinessUnit = q.BusinessUnit;

            this.NewLocation = q.NewLocation;
            this.Dealer = q.Dealer;
            this.TaxExempt = q.TaxExempt;

            this.linesHW = q.linesHW;
            this.linesAcc = q.linesAcc;
            this.linesSW = q.linesSW;
            this.linesCC = q.linesCC;
            this.linesInst = q.linesInst;
            this.linesRec = q.linesRec;
        }
        
        public void AddLineItem(LineItem line)
        {
            line.Product.Cost = line.Product.GetCost(line.Product.PartNumber);
            
            switch (line.Product.Category)
                {
                    case "Hardware":
                        this.linesHW.Add(line);
                        break;
                    case "Accessories":
                        this.linesAcc.Add(line);
                        break;
                    case "Software":
                        this.linesSW.Add(line);
                        break;
                    case "ContentCreation":
                        this.linesCC.Add(line);
                        break;
                    case "Installation":
                        this.linesInst.Add(line);
                        break;
                    case "Recurring":
                        this.linesRec.Add(line);
                        break;
                    default: //just to find anything that doesn't get put where it should be. 
                        line.Product.Name += " (ERROR)";
                        this.linesHW.Add(line);
                        break;
                }
        }
        public void RemoveLineItem(string category, int index)
        {
            switch (category)
            {
                case "Hardware":
                    this.linesHW.RemoveAt(index);
                    break;
                case "Accessories":
                    this.linesAcc.RemoveAt(index);
                    break;
                case "Software":
                    this.linesSW.RemoveAt(index);
                    break;
                case "ContentCreation":
                    this.linesCC.RemoveAt(index);
                    break;
                case "Installation":
                    this.linesInst.RemoveAt(index);
                    break;
                case "Recurring":
                    this.linesRec.RemoveAt(index);
                    break;
            }
        }

        public double TotalLines(string category)
        {
            double total = 0;

            switch (category)
            {
                case "Hardware":
                    foreach (LineItem line in linesHW)
                        total += line.Total;
                    break;
                case "Accessories":
                    foreach (LineItem line in linesAcc)
                        total += line.Total;
                    break;
                case "Software":
                    foreach (LineItem line in linesSW)
                        total += line.Total;
                    break;
                case "ContentCreation":
                    foreach (LineItem line in linesCC)
                        total += line.Total;
                    break;
                case "Installation":
                    foreach (LineItem line in linesInst)
                        total += line.Total;
                    break;
                case "Recurring":
                    foreach (LineItem line in linesRec)
                        total += line.Total;
                    break;
            }

            return total;
        }

        public double GetGrandTotal()
        {
            return
               TotalLines("Hardware") +
               TotalLines("Accessories") +
               TotalLines("Software") +
               TotalLines("ContentCreation") +
               TotalLines("Installation") +
               TotalLines("Recurring");
        }

        public double GetGrandMargin()
        {
            double grandCost =0;

            foreach(LineItem line in linesHW)
                grandCost += line.Product.Cost*line.Quantity;
            foreach (LineItem line in linesAcc)
                grandCost += line.Product.Cost * line.Quantity;
            foreach (LineItem line in linesSW)
                grandCost += line.Product.Cost * line.Quantity;
            foreach (LineItem line in linesCC)
                grandCost += line.Product.Cost * line.Quantity;
            foreach (LineItem line in linesInst)
                grandCost += line.Product.Cost * line.Quantity;
            foreach (LineItem line in linesRec)
                grandCost += line.Product.Cost * line.Quantity;

            return GetGrandTotal() - grandCost;
        }

    }
}