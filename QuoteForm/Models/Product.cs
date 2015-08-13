using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuoteForm.Models
{
    public class Product
    {
        IDocumentSession session = QuoteForm.DataDocumentStore.Instance.OpenSession();

        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int    PartNumber { get; set; }
        public double Price { get; set; }
        public double Cost { get; set; }
        public int    DefaultQuantity { get; set; }

        public Product()
        {

        }
        
        public Product(Product p)
        {
            this.Id = p.Id;
            this.Category = p.Category;
            this.Name = p.Name;
            this.PartNumber = p.PartNumber;
            this.Price = p.Price;
            this.Cost = p.Cost;
            this.DefaultQuantity = p.DefaultQuantity;
        }

        public double GetCost(int partno)
        {
            Product p = session.Query<Product>()
                .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                .Where(x => x.PartNumber == partno)
                .FirstOrDefault();

            return p.Cost;
        }
    }
}