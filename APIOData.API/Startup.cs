using APIOData.API.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOData.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConStr"]);
            });
            services.AddOData();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var builder = new ODataConventionModelBuilder();
            // categoriesController
            //[entity set Name] Controller
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Category>("Products");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Birinci "odata" routeName olarak veriyoruz.
                //Ýkinci "odata" ise www.your_awesome_domain.com/odata/entityName sorgulama yapacaðýmýz prefix'i tanýmlamak için veriyoruz
                endpoints.Select().Expand().OrderBy().MaxTop(null).Count();
                endpoints.MapODataRoute("odata", "odata", builder.GetEdmModel());
                endpoints.MapControllers();
            });
        }
    }
}
