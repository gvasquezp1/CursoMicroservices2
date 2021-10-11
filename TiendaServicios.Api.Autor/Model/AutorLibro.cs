using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Autor.Model
{
    public class AutorLibro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string  Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<GradoAcademico> ListaGradosAcademicos { get; set; }
        public string AutorGuid { get; set; }
    }
}
