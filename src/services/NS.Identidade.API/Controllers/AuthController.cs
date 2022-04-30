using Microsoft.AspNetCore.Mvc;
using NS.Identidade.API.Models;
using Microsoft.Extensions.Options;
using NS.Identidade.API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace NS.Identidade.API.Controllers;

[ApiController]
[Route("api/identidade")]
public class AuthController : AuthControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(IOptions<AppSettings> appSettings,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager) : base(appSettings, userManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

        if (result.Succeeded)
        {
            return CustomResponse(await GerarJwt(usuarioRegistro.Email));
        }

        foreach (var error in result.Errors)
        {
            AdicionarErroProcessamento(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
            false, true);

        if (result.Succeeded)
        {
            return CustomResponse(await GerarJwt(usuarioLogin.Email));
        }

        if (result.IsLockedOut)
        {
            AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse();
        }

        AdicionarErroProcessamento("Usuário ou Senha incorretos");
        return CustomResponse();
    }
}