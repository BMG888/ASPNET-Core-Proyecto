using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaLibrosMVC.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Autor { get; set; }

        public string ISBN { get; set; }
    }
}
