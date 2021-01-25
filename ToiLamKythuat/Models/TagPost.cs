using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class TagPost
    {
        public int tagId { get; set; }
        public long postId { get; set; }
        public virtual Tag tag { get; set; }
        public virtual Post post { get; set; }
    }
}
