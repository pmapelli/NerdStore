using MediatR;
using NS.Core.Mediator;
using NS.Clientes.API.Data;
using NS.Clientes.API.Models;
using FluentValidation.Results;
using NS.Clientes.API.Data.Repository;
using NS.Clientes.API.Application.Events;
using NS.Clientes.API.Application.Commands;

namespace NS.Clientes.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

        services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ClientesContext>();
    }
}