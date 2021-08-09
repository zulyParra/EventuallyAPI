using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EventuallyAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EventuallyAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options: options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserArea>().HasKey(x => new { x.UserId, x.AreaId });
            builder.Entity<Area>().HasData(new Area
            {
                Id=1,
                Name="Programacion"
            },new Area {
                Id = 2,
                Name = "Dibujo Digital"
            },new Area {
                Id = 3,
                Name = "Dibujo Tradicional"
            }, new Area {
                Id = 4,
                Name = "Animacion 3D"
            }, new Area {
                Id = 5,
                Name = "MOdelado 3D"
            });
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<UserArea> UserAreas { get; set; }
    }
}
