using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;

namespace CleanArchiteture.Structure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CleanArchitetureContext context) : base(context)
        {
        }
    }
}
