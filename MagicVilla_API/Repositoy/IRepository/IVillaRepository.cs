using MagicVilla_API.Models;

namespace MagicVilla_API.Repositoy.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
