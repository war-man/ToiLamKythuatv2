using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class Tag
    {
        public int id { get; set; }
        public string tagName { get; set; }
        public virtual ICollection<Post> posts { get; set; }
    }
}
