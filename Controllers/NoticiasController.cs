using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RUJulius2grow.Models;
using RUJulius2grow.Models.DTOs;
using TodoApp.Data;

namespace RUJulius2grow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper mapper;

        public NoticiasController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Noticias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Noticias>>> GetNoticias()
        {
            var noticias = await _context.Noticias.ToListAsync();
            return mapper.Map<List<Noticias>>(noticias);
        }

        // GET: api/Noticias/5
        [HttpGet("{id}",Name ="GetNoticias")]
        public async Task<ActionResult<Noticias>> GetNoticias(int id)
        {
            var noticia = await _context.Noticias.ToListAsync();
            return Ok(noticia);
        }

        // PUT: api/Noticias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutNoticias(int id, Noticias user)
        {
            if (id != user.IdNoticias)
                return BadRequest();

            var existNoticia = await _context.Noticias.FirstOrDefaultAsync(x => x.IdNoticias == id);

            if (existNoticia == null)
                return NotFound();
            
            existNoticia.IdUsuario = user.IdUsuario;
            existNoticia.Titulo = user.Titulo;
            existNoticia.Descripcion = user.Descripcion;
            existNoticia.Imagen = user.Imagen;
            existNoticia.FechaRegistro = user.FechaRegistro;
           
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetNoticias", new { user.IdNoticias }, user);
        }

        // POST: api/Noticias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostNoticias(Noticias data)
        {
            if (ModelState.IsValid)
            {

                var noticias = new Noticias() { IdUsuario = data.IdUsuario,Titulo = data.Titulo, Descripcion = data.Descripcion,Imagen = data.Imagen,FechaRegistro = data.FechaRegistro };
                await _context.Noticias.AddAsync(noticias);
                await _context.SaveChangesAsync();
               // ViewData["IdRol"] = new SelectList(_context.Rol, "IdRol", "Nombre", usuario.IdRol);
                return CreatedAtAction("GetNoticias", new {data.IdNoticias }, data);
            }

            return new JsonResult("No se pudo guardar la noticia") { StatusCode = 500 };
        }

        // DELETE: api/Noticias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticias(int id)
        {
            var existData = await _context.Noticias.FirstOrDefaultAsync(x => x.IdNoticias == id);
           
            if (existData == null)
                return NotFound();

            _context.Noticias.Remove(existData);
            await _context.SaveChangesAsync();

            return Ok(existData);
        }

        private bool NoticiasExists(int id)
        {
            return _context.Noticias.Any(e => e.IdNoticias == id);
        }
    }
}
