using Microsoft.AspNetCore.Mvc;
using NS.Identidade.API.Models;
using Microsoft.AspNetCore.Identity;

namespace NS.Identidade.API.Controllers;

[Route("api/identidade")]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro,
                                                UserManager<IdentityUser> userManager,
                                                SignInManager<IdentityUser> signInManager)
    {
        if (!ModelState.IsValid) return BadRequest();

        var user = new IdentityUser
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

        if (!result.Succeeded) return BadRequest();

        await _signInManager.SignInAsync(user, false);
        return Ok();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
            false, true);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest();
    }
}