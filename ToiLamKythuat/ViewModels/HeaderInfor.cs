using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.ViewModels
{
    public class HeaderInfor
    {
        public string Title { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> NewsKeywords { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string SiteName { get; set; }
        public string AppTitle { get; set; }
        public string ArticleId { get; set; }
        public string CategoryId { get; set; }
        public string ContentType { get; set; }
        public string DatePublished { get; set; }
        public string DateModified { get; set; }
        public string DateCreated { get; set; }
        public string Icon { get; set; }
        #region Facebook
        public string FacebookTitle { get; set; }
        public string FacebookDescription { get; set; }
        public string FacebookThumnailImage { get; set; }
        public string FacebookPushlishser { get; set; }
        public string FacebookImageWidth { get; set; }
        public string FacebookImageHeight { get; set; }
        public string FacebookDatePublish { get; set; }
        public string FacebookLastModified { get; set; }
        public List<string> FacebookTags { get; set; }
        #endregion

        #region geo
        public string GeoName { get; set; }
        public string GeoRegion { get; set; }
        public string GeoPosition { get; set; }
        public string ICBM { get; set; }
        #endregion

        #region twitter
        public string TwitterUrl { get; set; }
        public string TwitterTitle { get; set; }
        public string TwitterDescription { get; set; }
        public string TwitterSite { get; set; }
        public string TwitterCreator { get; set; }
        public string TwitterImage { get; set; }
        #endregion
    }
}
