using Polly;
using NS.WebApp.MVC.Services;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace NS.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

        services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //.AddTransientHttpErrorPolicy(
            //p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
            .AddPolicyHandler(PollyExtensions.EsperarTentar())
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

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