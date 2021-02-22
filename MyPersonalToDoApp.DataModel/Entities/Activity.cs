using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.Entities
{
    public class Activity : IEntityBase
    {
        public Activity()
        {
            this.Todos = new List<Todo>();
        }
        public long Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }        
        public DateTime Created { get; set; }        
        public DateTime? Expiration { get; set; }
        public Status Status { get; set; }
        public ICollection<Todo> Todos { get; set; }
        public DateTime LastUpdate { get; set; }


    }
}
