using NS.Catalogo.API.Data;
using NS.Catalogo.API.Models;
using NS.Catalogo.API.Repository;

namespace NS.Catalogo.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<CatalogoContext>();
    }
}