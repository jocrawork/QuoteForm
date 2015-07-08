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
        public List<LineItem> Lines = new List<LineItem>();

        public string Id { get; set; }
        public string Owner { get; set; }
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
        public bool TaxExempt { get; set; }

        public int GrandTotal { get; set; } //TODO: make this work

        public Quote()
        {
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

            this.GrandTotal = q.GrandTotal;
        }
        
        public Quote AddLineItem(LineItem line)
        {
            line.Total = line.Product.Price * line.Quantity;
            this.Lines.Add(line);

            return this;
        }

    }
}