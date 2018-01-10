﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Models
{
    public class MoviesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MoviesContext() : base("name=MoviesContext")
        {
            Database.SetInitializer<MoviesContext>(null);
        }

        public System.Data.Entity.DbSet<Movies.Models.Movie> Movies { get; set; }
        public System.Data.Entity.DbSet<Movies.Models.Customer> Customers { get; set; }
        //public System.Data.Entity.DbSet<Movies.Models.CustomerMovie> MovieCustomers { get; set; }

    }
}
