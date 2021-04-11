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
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Status { get; set; }
        public string LastUpdate { get; set; }
    }
}
