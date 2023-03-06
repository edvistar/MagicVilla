using MagicVilla_API.Models;

namespace MagicVilla_API.Repositoy.IRepository
{
    public interface INumeroVillaRepository : IRepository<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
    }
}
