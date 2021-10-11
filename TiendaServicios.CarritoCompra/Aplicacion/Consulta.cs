using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.CarritoCompra.Modelo;
using TiendaServicios.CarritoCompra.Persistencia;
using TiendaServicios.CarritoCompra.RemoteInterface;

namespace TiendaServicios.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta,CarritoDto>
        {
            private readonly CarritoContexto _context;
            private readonly ILibrosService _libroService;

            public Manejador(CarritoContexto context, ILibrosService libroService)
            {
                _context = context;
                _libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion =await _context.CarritoSesion
                    .FirstOrDefaultAsync(e => e.CarritoSesionId == request.CarritoSesionId);
                
                if(carritoSesion==null)
                        throw new Exception("Id carrito sesion no encontrada");

                var carri = await _context.CarritoSesion.ToListAsync();
                var carritoSesionDetalle = await _context.CarritoSesionDetalle
                    .Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                List<CarritoDetalleDto> list = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response=await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objLibro = response.Libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                            TituloLibro = objLibro.Titulo,
                            LibroId = objLibro.Id,
                            FechaPublicacion = objLibro.FechaPublicacion
                        };
                        list.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = list
                };

                return carritoSesionDto;
            }
        }
    }
}
