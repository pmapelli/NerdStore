using NS.Core.Messages;

namespace NS.Clientes.API.Application.Events;

public class ClienteRegistradoEvent : Event
{
    public Guid Id { get; }
    public string Nome { get; }
    public string Email { get; }
    public string Cpf { get; }

    public ClienteRegistradoEvent(Guid id, string nome, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }
}