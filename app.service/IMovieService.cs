using System;
using System.Collections.Generic;
using System.Text;
using app.service.Movies.Commands.CreateMovie;
using app.service.Movies.Commands.EditMovie;
using app.service.Movies.Query.GetMovieById;

namespace app.service
{
    public interface IMovieService
    {
        CreateMovieResult CreateMovie(CreateMovieCommand movie);
        GetMovieByIdResult GetMovieById(GetMovieByIdQuery query);
        EditMovieResult EditMovie(EditMovieCommand movie);
    }
}
