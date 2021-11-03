using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUJulius2grow.Models.DTOs.Responses;
using RUJulius2grow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models.DTOs.Requests;

namespace RUJulius2grow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IUsuarioService _userService;
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context, IUsuarioService usrservice) 
        {
            _userService = usrservice;
            _context = context;
        }

        [HttpPost("login")]

        public IActionResult Autetificar([FromBody] UsuarioRegistrationDto user)
        {
            Respuesta respuesta = new Respuesta();
            var usuarioResponse = _userService.Auth(user);

            if (usuarioResponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o Password Incorrectos";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = usuarioResponse;
            return Ok(respuesta);
        }

    
    }
}
