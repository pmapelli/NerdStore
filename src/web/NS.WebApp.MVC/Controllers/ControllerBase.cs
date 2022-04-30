using NS.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace NS.WebApp.MVC.Controllers;

public class ControllerBase : Controller
{
    protected bool ResponsePossuiErros(ResponseResult? resposta)
    {
        if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

        foreach (var mensagem in resposta.Errors.Mensagens)
        {
            ModelState.AddModelError(string.Empty, mensagem);
        }

        return true;
    }
}