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
    }
}
