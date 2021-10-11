using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Model;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico:IRequest<LibroMaterialDTO>
        {
            public Guid? Id{ get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {
            private readonly ContextoLibreria _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libros = await _context.LibroMaterial.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (libros== null)
                    throw new Exception("No se encontro el libro :" + request.Id);

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libros);

                return libroDto;

            }
        }
    }
}
