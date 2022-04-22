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
    public class ShowsClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ShowsClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://api.tvmaze.com");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IList<Show>> GetShowsPerPage(int page)
        {

            List<Show> shows = null;

            try
            {
                using (var response = await _client.GetAsync($"shows?page={page}", HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    shows = await System.Text.Json.JsonSerializer.DeserializeAsync<List<Show>>(stream, _options);
                }
            }
            catch (Exception)
            {

                throw;
            }

           

            return shows;
        }
    }
}
