using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.CarritoCompra.Modelo;
using TiendaServicios.CarritoCompra.Persistencia;

namespace TiendaServicios.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _context;

            public Manejador(CarritoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var sesion = new CarritoSesion
                {
                    FechaCreacion=request.FechaCreacionSesion,
                    
                };

                _context.CarritoSesion.Add(sesion);
                var result=await _context.SaveChangesAsync();

                if (result == 0)
                        throw new Exception("Error en la insercion en el carrito de compras");

                var id=sesion.CarritoSesionId;

                foreach (var item in request.ProductoLista)
                {
                    var detalles = new CarritoSesionDetalle
                    {
                        CarritoSesionId = id,
                        FechaCreacion = DateTime.Now,
                        ProductoSeleccionado = item
                    };
                    _context.CarritoSesionDetalle.Add(detalles);
                }
                result = await _context.SaveChangesAsync();

                
                if (result > 0)
                    return Unit.Value;
    
                throw new Exception("no se puedo completar el detalle sesion");


            }
        }
    }
}
