using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserService;

namespace dotnet.highloadcup.one
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IUserProvider, UsersProvider>();
            PreloadData(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
            
        }

        private void PreloadData(IServiceCollection services)
        {
            DirectoryInfo dr = new DirectoryInfo("/tmp/data");
            if (dr.Exists)
            {
                var userProvider = services.BuildServiceProvider().GetService<IUserProvider>();
                userProvider.LoadUser(new List<Common.User>());
            }
            else
                Console.WriteLine("Directory not found!");
        }
    }
}
