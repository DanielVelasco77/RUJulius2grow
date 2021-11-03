using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;                    
using Microsoft.EntityFrameworkCore;
using RUJulius2grow.Models.DTOs.Requests;
using RUJulius2grow.Models.DTOs.Responses;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Tools;

namespace RUJulius2grow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public UsuariosController(ApiDbContext context,
            IMapper mapper)
        {
            _context = context;
             _mapper = mapper;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            var usuario = await _context.Usuario.ToListAsync();
            return _mapper.Map<List<Usuario>>(usuario);
        }

        // GET: api/Usuarios/5

        [HttpGet("{id}", Name = "GetUsuario")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.ToListAsync();
            return Ok(usuario);

        }


        // PUT: api/Usuarios/
    
        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuario(int id, Usuario user)
        {
            if (id != user.IdUsuario)
                return BadRequest();

            var existUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (existUsuario == null)
                return NotFound();
            string spass = Encrypt.GetSHA256(user.Password);
            existUsuario.Nombre = user.Nombre;
            existUsuario.Email = user.Email;
            existUsuario.Password = spass;           
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUsuario", new { user.IdUsuario }, user);
          
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult> PostUsuario([FromBody]Usuario user)
        {
            var listEmail = await LoadAllEmail();
            Respuesta respuesta = new Respuesta();
            if (ModelState.IsValid)
            {

                foreach (var dato in listEmail.Usuario.ToList())
                {
                    if (dato.Email == user.Email)
                    {
                        respuesta.Mensaje = "El usuario ya esta registrado";
                        return BadRequest(respuesta);

                    };
                }
                string spass = Encrypt.GetSHA256(user.Password);

                var usuario = new Usuario() { Nombre = user.Nombre, Email = user.Email, Password = spass };
                await _context.Usuario.AddAsync(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUsuario", new { user.IdUsuario }, user);
            }
             

                return new JsonResult("No se pudo Guardar el Usuario") { StatusCode = 500 };
          

         }

        private bool EmailExists(string email)
        {
            return _context.Usuario.Any(e => e.Email == email);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var existItem = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (existItem == null)
                return NotFound();

            _context.Usuario.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }

        private async Task<EmailDto> LoadAllEmail()
        {
            var viewModel = new EmailDto();
            viewModel.Usuario = await _context.Usuario.ToListAsync();
            return viewModel;
        }
    }
    
}
