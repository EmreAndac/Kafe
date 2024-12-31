using Kafe.BL.Manager.Abstract;
using Kafe.BL.Manager.Concrete;

namespace Kafe.MVC.Extensions
{
    public static class KafeExtensions
    {
        public static IServiceCollection AddKafeService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IManager<>), typeof(Manager<>));

            return services;
        }

    }
}