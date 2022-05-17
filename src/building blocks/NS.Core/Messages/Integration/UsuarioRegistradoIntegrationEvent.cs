namespace NS.Core.Messages.Integration;

public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; }
    public string Nome { get; }
    public string Email { get; }
    public string Cpf { get; }

    public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string email, string cpf)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Cpf = cpf;
    }
}