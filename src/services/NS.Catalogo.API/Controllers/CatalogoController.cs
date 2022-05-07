using NS.Catalogo.API.Models;
using Microsoft.AspNetCore.Mvc;
using NS.WebAPI.CORE.Identidade;
using Microsoft.AspNetCore.Authorization;

namespace NS.Catalogo.API.Controllers;

[ApiController]
[Authorize]
[Route("catalogo/produtos")]
public class CatalogoController : Controller
{
    private readonly IProdutoRepository _produtoRepository;

    public CatalogoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<Produto>> Index()
    {
        return await _produtoRepository.ObterTodos();
    }

    [ClaimsAuthorize("Catalogo", "Ler")]
    [HttpGet("{id:guid}")]
    public async Task<Produto> ProdutoDetalhe(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }
}