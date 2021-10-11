using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Persistence
{
    public class ContextAutor:DbContext
    {
        public ContextAutor(DbContextOptions<ContextAutor> options):base(options)
        {

        }

        public DbSet<AutorLibro> Autors { get; set; }
        public DbSet<GradoAcademico>  GradosAcademicos { get; set; }
    }
}
