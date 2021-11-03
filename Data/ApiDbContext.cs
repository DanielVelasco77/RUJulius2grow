using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RUJulius2grow.Models;
using RUJulius2grow.Models.DTOs;
using System;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Models.DTOs.Requests;

namespace TodoApp.Data
{
    public class ApiDbContext : DbContext
    {
     
    

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Usuario> Usuario { get; set; }

       

       // public virtual DbSet<UsuarioRegistrationDto> UsuarioRegistrationDto { get; set; }

        public virtual DbSet<Noticias> Noticias { get; set; }
        public virtual DbSet<TokenRequest> RefreshTokens { get; set; }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Noticias>().HasKey(m => m.IdNoticias);
            base.OnModelCreating(builder);
        }

    }
}