using AUBAssignmentBusiness.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static AUBAssignmentBusiness.DataAccess.AUBAssignmentDBContext;

namespace AUBAssignmentBusiness
{
    public class RestClient
    {
        private HttpClient client;

        public RestClient(HttpClient client)
        {
            this.client = client;
        }

        record genresList(string name, List<GenresModel> genres);
        record moviesList(int page, List<MoviesModel> results);

        public async Task<List<GenresModel>> GetListOfGenres()
        {
            var message = await client.GetAsync("/3/genre/movie/list");
            if (!message.IsSuccessStatusCode)
                throw new ApplicationException(await message.Content.ReadAsStringAsync());

            var result = await message.Content.ReadFromJsonAsync<genresList>();

            return result.genres;
        }

        public async Task<List<MoviesModel>> GetListOfPopularMovies()
        {
            var message = await client.GetAsync("/3/movie/popular");
            if (!message.IsSuccessStatusCode)
                throw new ApplicationException(await message.Content.ReadAsStringAsync());

            var result = await message.Content.ReadFromJsonAsync<moviesList>();

            return result.results;
        }

    }
}
