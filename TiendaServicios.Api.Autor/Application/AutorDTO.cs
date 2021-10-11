using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Application
{
    public class AutorDTO
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorGuid { get; set; }
    }
}
