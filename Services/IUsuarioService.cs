using RUJulius2grow.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models.DTOs.Requests;

namespace RUJulius2grow.Services
{
    public interface IUsuarioService
    {
        UsuarioResponse Auth(UsuarioRegistrationDto model);
    }
}
