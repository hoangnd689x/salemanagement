using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Services;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Web
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
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddAuthentication(Code.AppVariables.AuthenticationScheme)
            .AddCookie(Code.AppVariables.AuthenticationScheme, options =>
             {
                 options.LoginPath = "/Auth/Index/";
                 ///options.Cookie.Domain = "contoso.com";
                 options.Cookie.Path = "/";
                 options.Cookie.HttpOnly = true;
                 //options.Cookie.SameSite = SameSiteMode.Lax;
                 //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
             });

            services.AddDbContext<Infrastructure.Data.BatTrang.ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetSection("BatTrangOnlineConnectionString").Value));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            
            // services.AddScoped<Core.Interfaces.HCPayment.ICustomerPackageRepository, Infrastructure.Data.HCPayment.CustomerPackageRepository>();


            //services.AddScoped(typeof(IRepository<>), typeof(Infrastructure.Data.BatTrang.BatTrangRepository<>));
            services.AddScoped(typeof(IBatTrangRepository<>), typeof(Infrastructure.Data.BatTrang.BatTrangRepository<>));
            services.AddScoped<Core.Interfaces.BatTrang.IProductRepository, Infrastructure.Data.BatTrang.ProductRepository>();
            services.AddScoped<Core.Interfaces.BatTrang.IRelatedProductRepository, Infrastructure.Data.BatTrang.RelatedProductRepository>();
            services.AddScoped<Core.Interfaces.BatTrang.IBillRepository, Infrastructure.Data.BatTrang.BillRepository>();
            services.AddScoped(typeof(IProductTypeRepository<>), typeof(Infrastructure.Data.BatTrang.ProductTypeRepository<>));
            services.AddScoped<Core.Interfaces.IImageRepository, Infrastructure.Data.BatTrang.ImageRepository>();


            services.AddTransient<Web.Interfaces.IProductTypeService, Web.Services.ProductTypeService>();
            services.AddTransient<Web.Interfaces.IProductService, ProductServie>();
            services.AddTransient<IBatTrangService, BatTrangService>();
            services.AddTransient<Code.Caching.IApiCache, Code.Caching.ApiCache>();

            services.AddMemoryCache();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index2}/{id?}");
            });
        }
    }
}
