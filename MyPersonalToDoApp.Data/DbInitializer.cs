using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data
{
    public class DbInitializer
    {
        public static void Init(ToDoContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
