using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string Id { get; set; }
    }
}
