using Microsoft.EntityFrameworkCore;
using SmartAdmin.Web.Models.Bufete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Web.Data
{
    public class BufeteDbContext:DbContext
    {
        public BufeteDbContext(DbContextOptions<BufeteDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Proceso> Proceso { get; set; }
        public virtual DbSet<DetalleProceso> DetalleProceso { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
