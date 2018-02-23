using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Starships.API
using Starships.API.Model;

namespace Starships.API.Service
{
    public class StarshipApiService : IStarshipService
    {
        const string API_URL = "http://swapi.co/api/starships";
        public async Task<IEnumerable<Starship>> Get()
        {
            return await ApiClient.Get<IEnumerable<Person>>(API_URL, (data) =>
            {
                JObject dymanicData = JObject.Parse(data);
                return dymanicData["results"].Select(item => new Person
                {
                    Id = ReadIdFromUrl((string)item["url"]),
                    Name = (string)item["name"]
                });
            });
        }

        private int ReadIdFromUrl(string url)
        {
            var uri = new System.Uri(url);
            var lastSegment = uri.Segments[uri.Segments.Length - 1];
            var id = Regex.Match(lastSegment, "\\d+").Value;
            return int.Parse(id);
        }

        public async Task<Person> Get(int id)
        {
            return await ApiClient.Get<Starship>($"{API_URL}{id}", (data) =>
            {
                JObject item = JObject.Parse(data);
                return new Starship
                {
                    Id = (int)item["id"],
                    Name = (string)item["name"],
                    Model = (string)item["model"],
                    StarshipClass = (string)item["starship_class"],
                    Manufacturer = (string)item["manufacturer"],
                    HyperdriveRating = (string)item["hyperdrive_rating"],
                };
            });
        }
    }
}
