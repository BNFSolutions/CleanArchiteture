using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;

namespace CleanArchiteture.Structure.Repositories
{
    public class ProdutoRepository : BaseRepository<Produtos>, IProdutoRepository
    {
        public ProdutoRepository(CleanArchitetureContext context) : base(context)
        {
        }
    }
}
