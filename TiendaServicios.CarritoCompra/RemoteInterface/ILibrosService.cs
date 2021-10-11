using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.CarritoCompra.RemoteModel;

namespace TiendaServicios.CarritoCompra.RemoteInterface
{
    public interface ILibrosService
    {
        Task<(bool resultado,LibroRemote Libro,string ErrorMessage)> GetLibro(Guid id);
    }
}
