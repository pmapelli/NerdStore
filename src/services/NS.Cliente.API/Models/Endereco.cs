using NS.Core.DomainObjects;

namespace NS.Clientes.API.Models;

public class Endereco : Entity
{
    public string Logradouro { get; }
    public string Numero { get; }
    public string Complemento { get; }
    public string Bairro { get; }
    public string Cep { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public Guid ClienteId { get; private set; }

    // EF Relation
    public Cliente Cliente { get; protected set; }

    public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
    }
}