﻿using NS.Core.Data;

namespace NS.Clientes.API.Models;

public interface IClienteRepository : IRepository<Cliente>
{
    void Adicionar(Cliente cliente);

    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente> ObterPorCpf(string cpf);
}