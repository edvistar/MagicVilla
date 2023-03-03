using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Datos
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Vila Real",
                    Detalle = "Detalle de la Villa",
                    ImageUrl = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    Amenidad = "",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now
                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Premium Vista a la piscina",
                     Detalle = "Detalle de la Villa",
                     ImageUrl = "",
                     Ocupantes = 5,
                     MetrosCuadrados = 50,
                     Tarifa = 200,
                     Amenidad = "",
                     DateCreate = DateTime.Now,
                     DateUpdate = DateTime.Now
                 }
            );
        }
    }
}
