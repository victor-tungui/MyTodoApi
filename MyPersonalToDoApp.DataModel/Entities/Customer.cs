using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.Entities
{
    public class Customer : IEntityBase
    {
        public long Id { get; set; }
        public DateTime LastUpdate { get; set; }

        public Customer()
        {
            this.Activities = new List<Activity>();
        }

        #region AspNet User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        #endregion

        #region Activities
        public ICollection<Activity> Activities { get; set; }
        #endregion

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
