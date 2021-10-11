using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Model
{
    public class LibreriaMaterial
    {
        public Guid? Id { get; set; }
        public string Titulo{ get; set; }
        public DateTime? FechaPublicacion{ get; set; }
        public Guid? AutorId { get; set; }
        
    }
}
