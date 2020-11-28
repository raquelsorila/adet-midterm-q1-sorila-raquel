﻿using System;
using System.Collections.Generic;
using System.Text;

namespace app.service.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string Rating { get; set; }
        public int? DirectorID { get; set; }

    }
}
