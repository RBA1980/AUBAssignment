using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUBAssignmentBusiness.DataAccess
{
    public class AUBAssignmentDBContext : DbContext
    {
        public AUBAssignmentDBContext(DbContextOptions<AUBAssignmentDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<GenresModel> Genres { get; set; }
        public DbSet<MoviesModel> Movies { get; set; }

    }
}
