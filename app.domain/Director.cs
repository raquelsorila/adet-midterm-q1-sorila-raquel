using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace app.domain
{
    public class Director : BaseEntity
    {
        public Director()
        {
           
            Movies = new HashSet<Movie>();
            
        }

        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
