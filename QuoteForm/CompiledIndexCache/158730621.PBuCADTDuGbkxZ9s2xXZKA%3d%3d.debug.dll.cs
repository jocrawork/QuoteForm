using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_Auto_Products_ByCategoryAndNameAndPartNumber : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_Products_ByCategoryAndNameAndPartNumber()
	{
		this.ViewText = @"from doc in docs.Products
select new {
	PartNumber = doc.PartNumber,
	Category = doc.Category,
	Name = doc.Name
}";
		this.ForEntityNames.Add("Products");
		this.AddMapDefinition(docs => 
			from doc in ((IEnumerable<dynamic>)docs)
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Products", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				PartNumber = doc.PartNumber,
				Category = doc.Category,
				Name = doc.Name,
				__document_id = doc.__document_id
			});
		this.AddField("PartNumber");
		this.AddField("Category");
		this.AddField("Name");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("PartNumber");
		this.AddQueryParameterForMap("Category");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("PartNumber");
		this.AddQueryParameterForReduce("Category");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("__document_id");
	}
}
