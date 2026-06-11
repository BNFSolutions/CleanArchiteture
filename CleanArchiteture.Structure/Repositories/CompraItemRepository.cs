using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;

namespace CleanArchiteture.Structure.Repositories
{
    public class CompraItemRepository : BaseRepository<CompraItem>, ICompraItemRepository
    {
        public CompraItemRepository(CleanArchitetureContext context) : base(context)
        {
        }
    }
}
