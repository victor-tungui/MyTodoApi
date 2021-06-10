using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class TodoCreationDTO
    {
        [Required]        
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public long ActivityId { get; set; }
    }

    public class TodoUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }
    }
}
