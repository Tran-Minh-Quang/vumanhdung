using Team12EUP.Mapper;

namespace Team12EUP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Đăng kí automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
            
            return services;
        }
    }
}
