using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Models
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public string hashKey { get; set; }
        public bool isActive { get; set; }
    }
}
