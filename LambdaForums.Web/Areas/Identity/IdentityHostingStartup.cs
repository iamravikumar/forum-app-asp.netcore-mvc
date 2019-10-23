using LambdaForums.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LambdaForums.Web.Areas.Identity.IdentityHostingStartup))]
namespace LambdaForums.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) =>
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            //    services.AddDefaultIdentity<IdentityUser>()
            //        .AddEntityFrameworkStores<ApplicationDbContext>();
            //});
        }
    }
}