using AUBAssignmentBusiness.DataAccess;
using AUBAssignmentBusiness.Engines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AUBAssignmentServices.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AUBAssignmentController : ControllerBase
    {
        private readonly AUBAssignmentEngine engine;

        List<GenresModel> genres;
        List<MoviesModel> movies;
        List<int> offlineList = new List<int>();


        public AUBAssignmentController(AUBAssignmentEngine engine)
        {
            this.engine = engine;
        }

        [HttpGet("GetListGenres")]
        public async Task<List<GenresModel>> GetListGender()
        {

            if (genres == null)
            {
                genres = await engine.GetListGender();
            }
            return genres;
        }

        [HttpGet("GetListPopularMovies")]
        public async Task<List<MoviesModel>> GetListPopularMovies()
        {
            if (movies == null)
            {
                movies = await engine.GetListPopularMovies();
            }
            return movies;
        }


        [HttpGet("GetMovieDetail")]
        public async Task<MovieDetail> GetMovieDetail(int id)
        {
            if (genres == null)
            {
                genres = await engine.GetListGender();
            }

            if (movies == null)
            {
                movies = await engine.GetListPopularMovies();
            }

            return (from movie in movies
                    join genre in genres
                    on movie.genre_ids.Any(gid => genres.Select(g => g.id).Contains(gid)) equals true
                    where movie.id == id
                    select new MovieDetail(

                        movie, string.Join(", ", genres.Where(g => movie.genre_ids.Contains(g.id)).Select(g => g.name))
                    )).FirstOrDefault();

        }
        [HttpPost("SetOfflineList")]
        public bool SetOfflineList(int id)
        {
            if (!offlineList.Contains(id)) { offlineList.Add(id); }
            return true;
        }
        public record MovieDetail(MoviesModel movie, string GenreName);

    }
}
