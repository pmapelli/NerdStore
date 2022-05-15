﻿using System.ComponentModel;
using NS.WebApp.MVC.Extensions;
using System.ComponentModel.DataAnnotations;

namespace NS.WebApp.MVC.Models;

public class UsuarioRegistro
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("Nome Completo")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [DisplayName("CPF")]
    [Cpf]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Senha { get; set; }

    [DisplayName("Confirme sua senha")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string SenhaConfirmacao { get; set; }
}