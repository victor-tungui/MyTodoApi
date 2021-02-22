using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel
{
    /// <summary>
    /// Interface base for all the model entities
    /// </summary>
    public interface IEntityBase
    {   
        long Id { get; set; }
        DateTime LastUpdate { get; set; }
    }
}
