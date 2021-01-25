using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class Post
    {
        public long id { get; set; }
        public string title { get; set; }
        public DateTime createDate { get; set; } 
        public string summary { get; set; }
        public string metaTitle { get; set; }
        public string description { get; set; }
        public string thumnailImage { get; set; }
        public string coverImage { get; set; }
        public string content { get; set; }
        public string keywords { get; set; }
        public string detail { get; set; }
        public virtual ICollection<Category> categories { get; set; }
        public virtual ICollection<Tag> tags { get; set; }
    }
}
