using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class Category
    {
        public string code { get; set; }
        public string name { get; set; }
        public Category parent { get; set; }
        public virtual ICollection<Post> posts { get; set; }
    }
}
