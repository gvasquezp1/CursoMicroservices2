using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextAutor _context;

            public Manejador(ContextAutor context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var modelo = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellidos=request.Apellido,
                    FechaNacimiento=request.FechaNacimiento,
                    AutorGuid=Guid.NewGuid().ToString()
                };

                _context.Autors.Add(modelo);
                var valor = await _context.SaveChangesAsync();
                if(valor>0)
                {
                    return Unit.Value;
                }
                throw new Exception("no se pudo insertar un nuevo libro");

            }
        }
    }
}
