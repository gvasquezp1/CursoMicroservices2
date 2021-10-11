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
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {

        }

        public class Manejador : IRequestHandler<ListaAutor,List<AutorDTO>>
        {
            private readonly ContextAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextAutor context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores= await _context.Autors.ToListAsync();
                var autoresDTO = _mapper.Map<List<AutorLibro>, List<AutorDTO>>(autores);
                return autoresDTO;

            }
        }


    }
}
