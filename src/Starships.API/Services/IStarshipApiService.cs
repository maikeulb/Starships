using System.Collections.Generic;
using System.Threading.Tasks;

namespace Starship.Services
{
    public interface IStarshipService
    {
        Task<IEnumerable<Starship>> Get();
        Task<Starship> Get(int id);
    }
}
