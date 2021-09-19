using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using System.Diagnostics;
using RentWebProj.Helpers;
[assembly: OwinStartup(typeof(RentWebProj.Startup))]

namespace RentWebProj
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            //RecurringJob.AddOrUpdate(() => Debug.WriteLine($"API現在時間：{DateTime.Now}"), Cron.Minutely);
            app.MapSignalR();

        }
    }
}
