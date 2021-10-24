using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwentyTwoSeven.Contracts;
using TwentyTwoSeven.Data.DataContext;
using TwentyTwoSeven.Infrastructure.Logger;
using TwentyTwoSeven.Services;

namespace TwentyTwoSeven.Di
{
    public static class DependencyContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void RegisterDbContext(this IServiceCollection services)
        {
            services.AddDbContext<RepoContext>(
                option => option.UseInMemoryDatabase(databaseName: "MemDb"),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient);
        }

      public static void RegisterConfigWrapper(this IServiceCollection services)
        {
            services.AddTransient<IRepoWrapper, RepoWrapper>();
        }

    }
}
