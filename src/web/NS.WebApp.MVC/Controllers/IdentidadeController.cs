using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace NS.WebApp.MVC.Controllers;

public class IdentidadeController : IdentidadeControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public IdentidadeController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpGet]
    [Route("nova-conta")]
    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
    {
        if (!ModelState.IsValid) return View(usuarioRegistro);

        UsuarioRespostaLogin? resposta = await _autenticacaoService.Registro(usuarioRegistro);

        if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

        await RealizarLogin(resposta);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UsuarioLogin usuarioLogin, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(usuarioLogin);

        UsuarioRespostaLogin? resposta = await _autenticacaoService.Login(usuarioLogin);

        if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

        await RealizarLogin(resposta);

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}