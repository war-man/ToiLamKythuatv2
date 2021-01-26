using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Models;

namespace ToiLamKythuat.ViewModels
{
    public class SidebarInfor
    {
        public List<Post> TopPosts { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
