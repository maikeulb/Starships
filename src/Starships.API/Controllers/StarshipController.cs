using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Starship.Model;
using Starship.Services;

namespace Starship.API.Controller
{
    [Route("api/[controller]")]
    public class StarshipController : Controller
    {
        private readonly IStarshipService _starshipService;
        public StarshipController(IStarshipService starshipService)
        {
            _starshipService = starshipService;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> Get()
        {
            return await _starshipService.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var starship = await _starshipService.Get(id);
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
