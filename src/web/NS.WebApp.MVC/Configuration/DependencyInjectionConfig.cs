using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Extensions;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
        services.AddHttpClient<ICatalogoService, CatalogoService>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }
}