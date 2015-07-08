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

public class Index_Auto_Quotes : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_Quotes()
	{
		this.ViewText = @"from doc in docs.Quotes
select new {

}";
		this.ForEntityNames.Add("Quotes");
		this.AddMapDefinition(docs => 
			from doc in ((IEnumerable<dynamic>)docs)
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Quotes", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				__document_id = doc.__document_id
			});
		this.AddField("__document_id");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("__document_id");
	}
}
