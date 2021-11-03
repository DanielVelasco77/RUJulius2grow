using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace RUJulius2grow.Models
{
    public class Noticias
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNoticias { get; set; }

        [Required(ErrorMessage = "El Usuario es obligatorio")]
        //[StringLength(50, ErrorMessage = "El {0} debe ser al menos {2} y maximo{1} caracteres", MinimumLength = 1)]
  
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }  
        public string Descripcion { get; set; }
    
        public string Imagen { get; set; }

        public DateTime FechaRegistro { get; set; }

        //public virtual Usuario Usuario { get; set; }
       


    }
}
