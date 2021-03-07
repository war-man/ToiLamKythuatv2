using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToiLamKythuat.Models;
using ToiLamKythuat.ViewModels;

namespace ToiLamKythuat.ViewModelExtensions
{
    public static class HeaderInforExtensions
    {
        public static HeaderInfor GetDefaultValue(this HeaderInfor infor)
        {
            infor.SiteName = AppGlobal.SiteName;
            infor.Icon = AppGlobal.SiteUrl + AppGlobal.SiteImage;
            infor.Keywords = AppGlobal.SiteKeywords;
            infor.FacebookTags = AppGlobal.SiteKeywords;
            infor.Description = AppGlobal.SiteDescription;
            infor.Url = AppGlobal.SiteUrl;
            infor.Title = AppGlobal.SiteTitle;
            return infor;
        }

        public static HeaderInfor GetPostValue(this HeaderInfor infor, Post post)
        {
            infor.SiteName = AppGlobal.SiteName;
            infor.Icon = AppGlobal.SiteImage;
            infor.Keywords = post?.keywords.Split(",").ToList();
            infor.Description = post?.description;
            infor.Title = post?.title;
            infor.ArticleId = post?.id.ToString();
            infor.DatePublished = post?.createDate.ToLongDateString();
            infor.Url = post?.metaTitle + "-" + post?.id + ".html";
            infor.DateCreated = post?.createDate.ToLongDateString();
            infor.DateModified = post?.createDate.ToLongDateString();
            infor.ContentType = post?.summary;

            infor.FacebookDatePublish = post?.createDate.ToLongDateString();
            infor.FacebookDescription = post?.description;
            infor.FacebookPushlishser = "https://www.facebook.com/toilamkythuat/";
            infor.FacebookLastModified = post?.createDate.ToLongDateString();
            infor.FacebookTags = post?.tags.Select(x => x.tagName).ToList();
            infor.FacebookTitle = post?.title;
            infor.FacebookThumnailImage = post?.thumnailImage;

            infor.TwitterTitle = post?.title;
            infor.TwitterImage = post?.thumnailImage;

            return infor;
        }
    }
}
