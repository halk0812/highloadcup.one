using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using Common;
using LocationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OptionsService;
using Swashbuckle.AspNetCore.Swagger;
using UserService;
using VisitService;

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
            services.AddMvc(options=> {
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IUserProvider, UsersProvider>();
            services.AddSingleton<IOptionProvider, OptionProvider>();
            services.AddSingleton<IVisitProvider, VisitProvider>();
            services.AddSingleton<ILocationProvider, LocationProvider>();
#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseBuffering();
#if DEBUG
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
#endif
            app.UseMvc();
            //int worker = 0;
            //int io = 0;
            //ThreadPool.GetAvailableThreads(out worker, out io);
            //Console.WriteLine("Thread pool threads available at startup: ");
            //Console.WriteLine("   Worker threads: {0:N0}", worker);
            //Console.WriteLine("   Asynchronous I/O threads: {0:N0}", io);
            PreloadData(app.ApplicationServices);
        }

        private void PreloadData(IServiceProvider services)
        {
            DirectoryInfo dr = new DirectoryInfo("/tmp/data");
            if (dr.Exists)
            {
                var optionProvider = services.GetService<IOptionProvider>();
                FileInfo optionFiles =  dr.GetFiles().FirstOrDefault(n => n.Extension.Contains("txt"));
                using(StreamReader stream = optionFiles.OpenText())
                {
                    string time = stream.ReadLine();
                    string type = stream.ReadLine();
                    optionProvider.SetGeneratedTime(int.Parse(time));
                    optionProvider.SetTypeLoad(int.Parse(type));
                }
                var userProvider = services.GetService<IUserProvider>();
                var locationProvider = services.GetService<ILocationProvider>();
                var visitProvider = services.GetService<IVisitProvider>();
                FileInfo zipFile = dr.GetFiles().FirstOrDefault(n => n.Extension.Contains("zip"));
                using (ZipArchive archive = ZipFile.Open(zipFile.FullName, ZipArchiveMode.Read))
                {
                    foreach (var entry in archive.Entries)
                    {
                        using (StreamReader writer = new StreamReader(entry.Open()))
                        {
                            string nameEntities = entry.FullName;

                            if (nameEntities.Contains("users"))
                            {
                                var jsonServices = JObject.Parse(writer.ReadToEnd())["users"];


                                var requiredServices = JsonConvert.DeserializeObject<List<User>>(jsonServices.ToString());
                                userProvider.LoadUser(requiredServices);

                                Console.WriteLine($"Add Users:{requiredServices.Count}");

                            }
                            if (nameEntities.Contains("locations"))
                            {
                                var jsonServices = JObject.Parse(writer.ReadToEnd())["locations"];

                                var requiredServices = JsonConvert.DeserializeObject<List<Location>>(jsonServices.ToString());
                                locationProvider.LoadLocations(requiredServices);

                                Console.WriteLine($"Add Locations:{requiredServices.Count}");

                            }
                            if (nameEntities.Contains("visits"))
                            {
                                var jsonServices = JObject.Parse(writer.ReadToEnd())["visits"];

                                var requiredServices = JsonConvert.DeserializeObject<List<Visit>>(jsonServices.ToString());
                                visitProvider.LoadVisits(requiredServices);

                                Console.WriteLine($"Add Visits:{requiredServices.Count}");

                            }
                        }

                    }
                }
                Console.WriteLine("LoadFinal");
            }
            else
                Console.WriteLine("Directory not found!");
        }
    }
}
