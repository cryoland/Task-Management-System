using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TMS.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace TMS.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
