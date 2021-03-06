using NS.MessageBus;
using Microsoft.AspNetCore.Mvc;
using NS.Identidade.API.Models;
using NS.WebAPI.CORE.Identidade;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using NS.Core.Messages.Integration;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace NS.Identidade.API.Controllers;

[ApiController]
[Route("api/identidade")]
public class AuthController : AuthControllerBase
{
    private readonly IMessageBus _bus;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(IMessageBus bus,
                            IOptions<AppSettings> appSettings,
                            UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager) : base(appSettings, userManager)
    {
        _bus = bus;
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

        IdentityResult? result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

        if (result.Succeeded)
        {
            ResponseMessage clienteResult = await RegistrarCliente(usuarioRegistro);

            if (clienteResult.ValidationResult.IsValid) return CustomResponse(await GerarJwt(usuarioRegistro.Email));

            await _userManager.DeleteAsync(user);
            return CustomResponse(clienteResult.ValidationResult);

        }

        foreach (IdentityError? error in result.Errors)
        {
            AdicionarErroProcessamento(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        SignInResult? result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha,
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

    private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistro usuarioRegistro)
    {
        IdentityUser? usuario = await _userManager.FindByEmailAsync(usuarioRegistro.Email);

        var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(
            Guid.Parse(usuario.Id), usuarioRegistro.Nome, usuarioRegistro.Email, usuarioRegistro.Cpf);

        try
        {
            return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);
        }
        catch
        {
            await _userManager.DeleteAsync(usuario);
            throw;
        }
    }
}