using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class ActivityDTO
    {
        public long Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expiration { get; set; }
        public Status Status { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
