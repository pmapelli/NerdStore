using NS.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace NS.Catalogo.API.Controllers;

[ApiController]
[Authorize]
public class CatalogoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [AllowAnonymous]
    [HttpGet("catalogo/produtos")]
    public async Task<IEnumerable<Produto>> Index()
    {
        return await _produtoRepository.ObterTodos();
    }

    [HttpGet("catalogo/produtos/{id}")]
    public async Task<Produto> ProdutoDetalhe(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}