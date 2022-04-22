using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TVMaze.Core.DTO;

namespace TVMaze.Infrastructure.Services.Clients
{
    public class CastClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public CastClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.tvmaze.com");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IList<Cast>> GetCastByShowId(int showId)
        {

            List<Cast> cast = null;

            using (var response = await _client.GetAsync($"/shows/{showId}/cast", HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                cast = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Cast>>(stream, _options);
            }

            return cast;
        }
    }
}
