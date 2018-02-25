using System.Collections.Generic;
using System.Threading.Tasks;
using Starships.API.Model;

namespace Starships.API.Service
{
    public interface IStarshipService
    {
        Task<Starship> GetStarship(int id);
        Task<IEnumerable<Starship>> GetStarships(int page);
    }
}
