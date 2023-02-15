using APIOData.API.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

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


            //Funcitons
            builder.EntityType<Category>().Collection.Function("CategoryCount").Returns<int>();


            builder.Function("GetKdv").Returns<int>();

            var MultiplyFunction = builder.EntityType<Product>().Function("MultiplyFunction");
            MultiplyFunction.Parameter<int>("a1");
            MultiplyFunction.Parameter<int>("a2");
            MultiplyFunction.Parameter<int>("a3");



            // categories()



            //--------------------------------///-------------------------
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Category>("Products");


            //Action
            //..../odata/categoty(1)/totalproductprice
            builder.EntityType<Category>().Action("TotalProductPrice").Returns<int>();
            builder.EntityType<Category>().Collection.Action("TotalProduct2").Returns<int>();

            //odata/categories/totalprice
            builder.EntityType<Category>().Collection.Action("TotalProductWithParametre").Returns<int>
                ().Parameter<int>("categotyId");




            builder.EntityType<Product>().Collection.Action("Login").Returns<string>().Parameter<Login>("UserLogin");


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
                endpoints.Select().Expand().OrderBy().MaxTop(null).Count().Filter();
                endpoints.MapODataRoute("odata", "odata", builder.GetEdmModel());
                endpoints.MapControllers();
            });
        }
    }
}
