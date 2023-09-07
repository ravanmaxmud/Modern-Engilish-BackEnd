using Microsoft.EntityFrameworkCore;
using ModernEngilish.Services.Abstracts;
using ModernEngilish.Services.Concretes;

namespace ModernEngilish.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
