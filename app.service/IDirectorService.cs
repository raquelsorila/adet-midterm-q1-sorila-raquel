using System;
using System.Collections.Generic;
using System.Text;
using app.service.Directors.Commands.CreateDirector;
using app.service.Directors.Commands.DeleteDirector;
using app.service.Directors.Commands.EditDirector;
using app.service.Directors.Query.GetDirectorById;

namespace app.service
{
    public interface IDirectorService
    {
        CreateDirectorResult CreateDirector(CreateDirectorCommand director);
        GetDirectorByIdResult GetDirectorById(GetDirectorByIdQuery query);
        DeleteDirectorResult DeleteDirector(DeleteDirectorCommand director);
        EditDirectorResult EditDirector(EditDirectorCommand director);
    }

}
