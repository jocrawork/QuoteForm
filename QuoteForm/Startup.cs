using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuoteForm.Startup))]
namespace QuoteForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
