using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RUJulius2grow.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Configuration;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Models.DTOs.Requests;
using TodoApp.Tools;

namespace RUJulius2grow.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ApiDbContext _context;

        public UsuarioService(IOptions<JwtConfig> jwtConfig, ApiDbContext context)
        {
            _jwtConfig = jwtConfig.Value;
            _context = context;
        }

        public UsuarioResponse Auth(UsuarioRegistrationDto model)
        {
                      

            UsuarioResponse usrResponse = new UsuarioResponse();
           //using (var db = new ApiDbContext())
           // {
                string spass = Encrypt.GetSHA256(model.Password);

                var usuario = new Usuario() { Email = model.Email, Password = model.Password };
                //usuario. = _context.Usuario.Where(p => p.Email == model.Email && p.Password == spass).FirstOrDefault();

                if (usuario == null) return null;

                usrResponse.Email = usuario.Email;
                usrResponse.Token = GetToken(usuario);
            //}

            return usrResponse;
        }

        private string GetToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Email, user.IdUsuario.ToString()),
                    }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    }

