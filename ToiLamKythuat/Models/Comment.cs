using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string content { get; set; }
        public Post post { get; set; }
        public Comment comment { get; set; }
    }
}
