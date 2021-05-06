using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public class ActivityFilterDTO
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }        

        public void ValidateRequest()
        {
            if (this.Page <= 0)
            {
                this.Page = 1;
            }

            if (this.PageSize <= 0 || this.PageSize > 1000)
            {
                this.PageSize = 10;
            }
        }
    }

    public class ActivityFilter : ActivityFilterDTO
    {
        public long CustomerId { get; set; }
    }
}
