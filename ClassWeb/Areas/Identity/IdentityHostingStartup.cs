using System;
using ClassWeb.Areas.Identity.Data;
using ClassWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ClassWeb.Areas.Identity.IdentityHostingStartup))]
namespace ClassWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ClassWebIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ClassWebIdentityContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>()
                //    .AddEntityFrameworkStores<ClassWebIdentityContext>();
                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ClassWebIdentityContext>()
        .           AddDefaultTokenProviders();

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddRazorPagesOptions(options =>
                    {
                        options.AllowAreas = true;
                        options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                        options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    });

                services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = $"/Identity/Account/Login";
                    options.LogoutPath = $"/Identity/Account/Logout";
                    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
                });

                // using Microsoft.AspNetCore.Identity.UI.Services;
                services.AddSingleton<IEmailSender, EmailSender>();
            

            });
        }
    }
}