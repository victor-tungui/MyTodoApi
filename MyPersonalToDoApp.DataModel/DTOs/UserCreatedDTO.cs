using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class UserCreatedDTO
    {
        public string UserId { get; set; }

        public UserCreatedDTO(string userId) => (UserId) = userId;
    }
}
