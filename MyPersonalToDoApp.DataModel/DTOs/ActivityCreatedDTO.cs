using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class ActivityCreatedDTO
    {
        public long Id { get; private set; }

        public ActivityCreatedDTO(long id) => (Id) = id;
    }
}
