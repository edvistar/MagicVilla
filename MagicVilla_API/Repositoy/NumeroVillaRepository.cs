using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repositoy.IRepository;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositoy
{
    public class NumeroVillaRepository : Repository<NumeroVilla>, INumeroVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.DateUpdate= DateTime.Now;
            _db.NumeroVilla.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

       
    }
}
