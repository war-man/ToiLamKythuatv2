using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.ViewModels;

namespace ToiLamKythuat.ViewModelExtensions
{
    public static class HeaderInforExtensions
    {
        public static HeaderInfor GetDefaultValue(this HeaderInfor infor)
        {
            infor.SiteName = AppGlobal.SiteName;
            infor.Icon = AppGlobal.SiteImage;
            infor.Keywords = AppGlobal.SiteKeywords;
            infor.FacebookTags = AppGlobal.SiteKeywords;
            infor.Description = AppGlobal.SiteDescription;
            infor.Url = AppGlobal.SiteUrl;
            return infor;
        }
    }
}
