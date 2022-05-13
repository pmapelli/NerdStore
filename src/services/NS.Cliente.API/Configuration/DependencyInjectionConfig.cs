using NS.Clientes.API.Data;
using NS.Clientes.API.Models;
using NS.Clientes.API.Data.Repository;

namespace NS.Clientes.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ClientesContext>();
    }
}