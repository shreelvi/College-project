using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Data;
using ClassWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace ClassWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public bool EnableDirectoryBrowsing { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDirectoryBrowser();

            //Reference: PeerVal Project
            // Add the following to start using a session.
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.2
            services.AddSession(sessOptions => {
                sessOptions.IdleTimeout = TimeSpan.FromSeconds(1000); // short time for testing. 
                //TimeSpan.FromMinutes(20) // default 20 minutes.
                sessOptions.Cookie.HttpOnly = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ClassWebContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ClassWebContextConnection")));
            services.AddTransient<IEmailService, EmailService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region Static Files Serve 
            //Created by: Elvis
            //Date Created: 03/16/2019
            //This configuratiion enables viewing of static files through URI
            //Reference:Followed and copied code from Microsoft doc to serve  
            //static files, enable Directory browsing and map file providers
            //Date Modified: 03/17 -- Added directory browsing to the UserDirectory folder
            //which contains each user default root folder
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-2.2


            // Set up custom content types - associating file extension to MIME type
            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".myapp"] = "application/x-msdownload";
            provider.Mappings[".html"] = "text/html";
            provider.Mappings[".txt"] = "text/txt";
            provider.Mappings[".image"] = "image/png";
            provider.Mappings[".js"] = "text/js";

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/MyImages",
                ContentTypeProvider = provider,
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/png"
            });

           //Enables directory browsing and files serve of upload folder via "baseurl/myFiles"
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload")),
                RequestPath = "/MyFiles",
                EnableDirectoryBrowsing = true
            });

            //Enables directory browsing of user directory folder via "baseurl/directory"
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserDirectory")),
                RequestPath = "/UserDirectory",
                EnableDirectoryBrowsing = true
            });
            #endregion

            app.UseSession(); // requred to have sessions in our application.

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
