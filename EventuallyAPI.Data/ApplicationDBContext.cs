using EventuallyAPI.Core.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                Id = 1,
                Name = "Programacion"
            }, new Area
            {
                Id = 2,
                Name = "Dibujo Digital"
            }, new Area
            {
                Id = 3,
                Name = "Dibujo Tradicional"
            }, new Area
            {
                Id = 4,
                Name = "Animacion 3D"
            }, new Area
            {
                Id = 5,
                Name = "Modelado 3D"
            });

            builder.Entity<SocialNetwork>().HasData(
                new SocialNetwork
                {
                    Id = 1,
                    Name = "Facebook"
                },
                 new SocialNetwork
                 {
                     Id = 2,
                     Name = "Linkedin"
                 },
                  new SocialNetwork
                  {
                      Id = 3,
                      Name = "Github"
                  },
                   new SocialNetwork
                   {
                       Id = 4,
                       Name = "Twitter"
                   },
                    new SocialNetwork
                    {
                        Id = 5,
                        Name = "Instagram"
                    }
                    , new SocialNetwork
                    {
                        Id = 6,
                        Name = "Discord"
                    }
            );

            builder.Entity<ComunitySocialNetwork>().HasKey
                (comunity => new { comunity.ComunityId, comunity.SocialNetworkId });
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<UserArea> UserAreas { get; set; }
        public DbSet<Comunity> Comunities { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<ComunitySocialNetwork> ComunitySocialNetworks { get; set; }
    }
}
