﻿using MagicVilla_API.Datos;
using MagicVilla_API.Models;
using MagicVilla_API.Repositoy.IRepository;
using System.Linq.Expressions;

namespace MagicVilla_API.Repositoy
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.DateUpdate= DateTime.Now;
            _db.Villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }

       
    }
}
