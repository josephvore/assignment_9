using System;
using System.Linq;

namespace final_assignment_3_413.Models
{
    public class EFMovieRepository : IMovieRepository
    { 
        //Of MovieDbContext type, create property Context
        private MovieDbContext _context;

        //Constructor
        public EFMovieRepository (MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<NewMovie> NewMovie => _context.NewMovie;

    }
}
