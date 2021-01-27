using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Helpers;
using ToiLamKythuat.Models;

namespace ToiLamKythuat.ModelExtensions
{
    public static class UserExtension
    {
        public static void HashPassword(this User user)
        {
            user.hashKey = Guid.NewGuid().ToString();
            user.password = SecurityHelper.Encrypt(user.hashKey, user.password);
        }
    }
}
