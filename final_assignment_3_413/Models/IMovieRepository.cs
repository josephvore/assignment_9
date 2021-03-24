using System;
using System.Collections.Generic;
using System.Linq;

namespace final_assignment_3_413.Models
{
    public interface IMovieRepository
    {
        IQueryable<NewMovie> NewMovie { get; }

    }
}
