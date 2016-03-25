using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace QuoteForm
{
    public class DataDocumentStore
    {
        private static IDocumentStore instance;

        public static IDocumentStore Instance
        {
            get
            {
                if (instance == null)
                    throw new InvalidOperationException("IDocumentStore has not been initialized.");

                return instance;
            }
        }

        public static IDocumentSession Initialize()
        {
            instance = new DocumentStore 
            {ConnectionStringName = "RavenDB",
            };

            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);

            instance.Conventions.MaxNumberOfRequestsPerSession = 1000; //this is frowned upon, default is 30. could have performance issues
            instance.Conventions.IdentityPartsSeparator = "-";
            instance.Initialize();
            
            return instance.OpenSession();
        }
    }

    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataDocumentStore.Initialize();
        }
    }
}