using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventuallyAPI.Core.Entities
{
    public class Comunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; }
        public string OtherArea { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public string Logo { get; set; }
        public string Banner { get; set; }
    }
}
