using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class CustomerMoviesViewModel
    {
        public Customer customer { get; set; }
        public List<Movie> moviesAvailable { get; set; }
        public List<Movie> moviesRented { get; set; }

    }
}