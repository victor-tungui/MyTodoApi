using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class EntityCreatedDTO
    {
        public long Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        public EntityCreatedDTO(long id, DateTime date) => (Id, CreationDate) = (id, date);
    }
}
