using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.CarritoCompra.RemoteModel
{
    public class LibroRemote
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Guid? AutorId { get; set; }

    }
}
