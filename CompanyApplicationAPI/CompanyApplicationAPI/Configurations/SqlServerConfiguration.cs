using CompanyApplication.Infra.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyApplicationAPI.Configurations
{
    public static class SqlServerConfiguration
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DBConnString"];
            services.AddDbContext<CompanyDBContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly("CompanyApplication.Infra.Domain");
                    x.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                });
            }, ServiceLifetime.Singleton);
        }
    }
}
