﻿using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;

namespace Blazor.Controllers
{
    public class LoginController : Controller
    {
        private readonly Config _config;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        LoginController(Config config)
        {
            config = config;
            _loginRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);

        }
        
        [HttpPost("/autenticar/validar")]
        public async Task<IActionResult> Validacion(Login login)
        {
            string rol = string.Empty;
            try
            {
                bool usuarioValido = await _loginRepositorio.ValidarUsuarioAsync(login);

                if (usuarioValido)
                {
                    Usuario user = await _usuarioRepositorio.GetPorCodigoAsync(login.CodigoUsuario);

                    if (user.EstaActivo)
                    {
                        rol = user.Rol;

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, user.CodigoUsuario),
                            new Claim(ClaimTypes.Role, user.Rol)
                        };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(10) });
                    }
                    else
                    {
                        return LocalRedirect("/Login/El usuario no se encuentra activo");
                    }
                }
                else
                {
                    return LocalRedirect("/Login/Datos de usuario invalidos");
                }
            }
            catch (Exception)
            {
            }
            return LocalRedirect("/");
        }

        [HttpGet("/Salir")]
        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/Login");
        }

    }
    }