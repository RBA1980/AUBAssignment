using AUBAssignmentBusiness.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AUBAssignmentBusiness.DataAccess.AUBAssignmentDBContext;

namespace AUBAssignmentBusiness.Engines
{
    public class AUBAssignmentEngine
    {
        private readonly AUBAssignmentDBContext context;
        private RestClient restClient;


        public AUBAssignmentEngine(AUBAssignmentDBContext context, RestClient restClient)
        {
            this.context = context;
            this.restClient = restClient;
        }

        public async Task<List<GenresModel>> GetListGender()
        {
            return await restClient.GetListOfGenres();
        }

        public async Task<List<MoviesModel>> GetListPopularMovies()
        {
            return await restClient.GetListOfPopularMovies();
        }

    }
}
