using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.Entities
{
    public class Todo : IEntityBase
    {        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Activity Activity { get; set; }
        public long ActivityId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
