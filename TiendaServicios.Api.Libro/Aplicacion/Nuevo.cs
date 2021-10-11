using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Model;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta:IRequest
        {
            public string Titulo { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public Guid? AutorLibro{ get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();

            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _context;

            public Manejador(ContextoLibreria context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var modelo = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    AutorId = request.AutorLibro,
                    FechaPublicacion = request.FechaPublicacion
                };

                var result = _context.LibroMaterial.Add(modelo); 
                
                var res2 = await _context.SaveChangesAsync();
                if (res2<=0)
                {
                    throw new Exception("No se pudo insertar libro");
                }
                return Unit.Value;

            }
        }
    }
}
