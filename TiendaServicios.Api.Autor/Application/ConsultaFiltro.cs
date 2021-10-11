using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;
using TiendaServicios.Api.Autor.Persistence;

namespace TiendaServicios.Api.Autor.Application
{
    public class ConsultaFiltro
    {

        public class AutorUnico : IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDTO>
        {
            private readonly ContextAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextAutor context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _context.Autors.Where(e => e.AutorGuid == request.AutorGuid).FirstOrDefaultAsync();
                var autorDTO = _mapper.Map<AutorLibro, AutorDTO>(autor);
                if (autor == null)
                    throw new Exception("No se encontro un autor por ese Id");

                return autorDTO;
            }
        }
    }

    
}
