using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;

namespace CleanArchiteture.Structure.Repositories
{
    public class CompraRepository : BaseRepository<Compra>, ICompraRepository
    {
        public CompraRepository(CleanArchitetureContext context) : base(context)
        {
        }
    }
}
