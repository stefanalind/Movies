using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Models
{

    //====================================================================================================================
    // Modell för Customer tabell samt Customer-views.
    //====================================================================================================================

    public class Customer
    {

        [Key]
        public int customerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Förnamn")]
        public string firstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Efternamn")]
        public string lastName { get; set; }

        public virtual ICollection<Movie> movies { get; set; }

        //-- Constructor --
        public Customer()
        {
            this.movies = new HashSet<Movie>();
        }

    }
}