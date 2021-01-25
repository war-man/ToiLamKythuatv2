using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class CategoryPost
    {
        public string categoryCode { get; set; }
        public long postId { get; set; }
        public virtual Post post { get; set; }
        public virtual Category category { get; set; }
    }
}
