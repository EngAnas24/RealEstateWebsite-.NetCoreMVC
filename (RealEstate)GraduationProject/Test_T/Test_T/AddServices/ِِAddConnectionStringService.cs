using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Entities.RealEstateEntities;
using RealEstate.Data.SqlDBContext;
using Test_T.Data;

namespace RealEstateWibsite.AddServices
{
    public static class ConnectionStringService
    {
        public static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<DBData>();

            // Register and populate List<RealEstateProperty> from the database
            services.AddSingleton<List<RealEstateProperty>>(provider =>
            {
                using (var scope = provider.CreateScope())
                {
                    var dbData = scope.ServiceProvider.GetRequiredService<DBData>();
                    // Ensure RealEstateProperty is accessible and populated
                    return dbData.RealEstateProperty.ToList();
                }
            });

            return services; // Don't forget to return the service collection!
        }
    }
}