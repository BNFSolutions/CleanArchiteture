using CleanArchiteture.Domain.Models;
using CleanArchiteture.Domain.Repositories.Interfaces;
using CleanArchiteture.Structure.Data;

namespace CleanArchiteture.Structure.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CleanArchitetureContext context) : base(context)
        {
        }
    }
}
