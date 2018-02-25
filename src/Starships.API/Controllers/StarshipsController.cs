using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Starships.API.Model;
using Starships.API.Service;
using Starships.API.Filters;

namespace Starships.API.Controllers
{
    [Route("api/[controller]")]
    public class StarshipsController : Controller
    {
        private readonly IStarshipService _starshipService;
        public StarshipsController(IStarshipService starshipService)
        {
            _starshipService = starshipService;
        }

        [HttpGet]
        [MyCacheFilter]
        public async Task<IEnumerable<Starship>> GetStarships(int? page)
        {
            var pageNumber = (page ?? 1);
            return await _starshipService.GetStarships(pageNumber);
        }

        [HttpGet("{id}")]
        [MyCacheFilter]
        public async Task<IActionResult> GetStarship(int id)
        {
            var starship = await _starshipService.GetStarship(id);

            if (starship == null)
            { 
                return NotFound(id); 
            }
            else 
            {
                return Json(starship);
            }
        }
    }
}
