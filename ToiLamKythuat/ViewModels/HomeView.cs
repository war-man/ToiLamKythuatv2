using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Models;

namespace ToiLamKythuat.ViewModels
{
    public class HomeView
    {
        public IEnumerable<Post> topPosts { get; set; }
        public IEnumerable<Post> posts { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Tag> tags { get; set; }
    }
}
