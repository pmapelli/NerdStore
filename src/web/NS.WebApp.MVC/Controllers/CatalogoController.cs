using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace NS.WebApp.MVC.Controllers
{
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogoService.ObterTodos();

            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            ProdutoViewModel produto = await _catalogoService.ObterPorId(id);

            return View(produto);
        }
    }
}