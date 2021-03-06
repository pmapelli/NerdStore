using NS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace NS.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    [Route("sistema-indisponivel")]
    public IActionResult SistemaIndisponivel()
    {
        var modelErro = new ErrorViewModel
        {
            Mensagem = "O sistema está temporariamente indisponível, isto pode ocorrer em momentos de sobrecarga de usuários.",
            Titulo = "Sistema indisponível.",
            ErroCode = 500
        };

        return View("Error", modelErro);
    }

    [Route("erro/{id:length(3,3):int}")]
    public IActionResult Error(int id)
    {
        var modelErro = new ErrorViewModel();

        switch (id)
        {
            case 500:
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
                break;
            case 404:
                modelErro.Mensagem =
                    "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
                break;
            case 403:
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
                break;
            default:
                return StatusCode(404);
        }

        return View("Error", modelErro);
    }
}