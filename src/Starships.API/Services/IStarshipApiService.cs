using System.Collections.Generic;
using System.Threading.Tasks;
using Starships.API.Model;

namespace Starships.API.Service
{
    public interface IStarshipService
    {
        Task<IEnumerable<Starship>> Get();
        Task<Starship> Get(int id);
    }
}
