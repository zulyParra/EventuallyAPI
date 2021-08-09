using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventuallyAPI.Core.Entities
{
    public class UserArea
    {
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
