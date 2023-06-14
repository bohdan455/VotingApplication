using BLL.Services;
using BLL.Services.Intefaces;
using DataAccess.Repositories.Intefaces;
using DataAccess.Repositories.Realisations.Main;

namespace WebApi.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static IServiceCollection AddWebRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPollRepository, PollRepository>();
            services.AddTransient<IChoiceRepository, ChoiceRepository>();
            return services;
        }
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<IPollService, PollService>();
            return services;
        }
    }
}
