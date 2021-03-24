using System;
using Microsoft.EntityFrameworkCore;

namespace final_assignment_3_413.Models
{
    //Movies DbContext inherits from DbContext
    public class MovieDbContext : DbContext
    {
        //Constructor that inherits from base
        public MovieDbContext (DbContextOptions<MovieDbContext> options) : base (options)
        {

        }

        //Create a DbSet of type NewMovie called NewMovie
        public DbSet<NewMovie> NewMovie { get; set; }
    }
}
