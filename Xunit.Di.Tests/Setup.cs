using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Xunit.Di.Tests
{
    public class Setup : Di.Setup
    {
        protected override void Configure()
        {
            ConfigureAppConfiguration((hostingContext, config) =>
            {
                bool reloadOnChange =
                    hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", true);

                if (hostingContext.HostingEnvironment.IsDevelopment())
                    config.AddUserSecrets<Setup>(true, reloadOnChange);
            });

            ConfigureServices((context, services) =>
            {
                services.AddSingleton<TextReaderService>();
            });
        }
    }
}