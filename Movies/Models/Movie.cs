using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class Movie
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Required]
        public int movieId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Genre")]
        public string genre { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Titel")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Längd (min)")]
        public int length { get; set; }

        [Required]
        [Display(Name = "Antal")]
        public int numberOf { get; set; }


        public virtual ICollection<Customer> customers { get; set; }

        //-- Constructor --
        public Movie()
        {
            this.customers = new HashSet<Customer>();
        }

    }

}