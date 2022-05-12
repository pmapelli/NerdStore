using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services.Handlers;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

        services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        //services.AddHttpClient("Refit",
        //        options =>
        //        {
        //            options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
        //        })
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
        //    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();
    }
}