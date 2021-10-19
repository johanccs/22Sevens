using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwentyTwoSeven.Data.DataContext;

namespace TwentyTwoSeven.Di
{
    public static class DependencyContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {

        }

        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<Context>(
                option => option.UseInMemoryDatabase(databaseName: "MemDb"),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);
        }

        //public static void RegisterSwagger(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(option =>
        //    {
        //        option.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo
        //        { 
        //            Title = "22Seven api",
        //            Version = "v1",
        //            Description = "Endpoint api for 22Seven banking app"
        //        });
        //    });
        //}
   
        //public static void ConfigureServices(this IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking info"));
        //}

    }
}
