﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUBAssignmentBusiness.DataAccess
{
    public class MoviesModel
    {
        [Key]
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public decimal popularity { get; set; }
        public string poster_path { get; set; }
        public DateTime release_date { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
        public int vote_count { get; set; }
    }
}
