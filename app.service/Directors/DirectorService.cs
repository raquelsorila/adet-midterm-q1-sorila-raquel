using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.domain;
using app.repository;
using app.service.Directors.Commands.CreateDirector;
using app.service.Directors.Commands.DeleteDirector;
using app.service.Directors.Commands.EditDirector;
using app.service.Directors.Query.GetDirectorById;

namespace app.service.Directors
{
    public class DirectorService : IDirectorService
    {
        private readonly IRepository<Director> _directorRepo;
        public DirectorService(IRepository<Director> directorRepo)
        {
            this._directorRepo = directorRepo;
        }
        public CreateDirectorResult CreateDirector(CreateDirectorCommand director)
        {
            var entity = new Director
            {
                Name = director.Name
            };

            _directorRepo.Create(entity);
            _directorRepo.SaveChanges();

            return new CreateDirectorResult
            {
                ID = entity.ID
            };

        }

        public DeleteDirectorResult DeleteDirector(DeleteDirectorCommand director)
        {
            throw new NotImplementedException();
        }

        public EditDirectorResult EditDirector(EditDirectorCommand director)
        {
            var entity = new Director
            {
                
                ID = director.ID,
                Name = director.Name
            };

            _directorRepo.Update(entity);
            _directorRepo.SaveChanges();

            return new EditDirectorResult
            {
            };
            
        }

        public GetDirectorByIdResult GetDirectorById(GetDirectorByIdQuery query)
        {
            var director = _directorRepo.GetAll().FirstOrDefault(d => d.ID == query.ID);

            if (director == null) return null;

            return new GetDirectorByIdResult
            {
                ID = director.ID,
                Name = director.Name
            };

        }
    }
}
