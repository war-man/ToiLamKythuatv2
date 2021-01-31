using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat.Controllers
{
    [Route("file-system")]
    public class FileSystemController : Controller
    {
        private readonly IWebHostEnvironment environment;

        public FileSystemController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("connector")]
        public async Task<IActionResult> Connector()
        {
            var connector = GetConnector();
            Request.RouteValues.Add("cmd", "open");
            return await connector.ProcessAsync(Request);
        }

        // Địa chỉ để truy vấn thumbnail
        // /el-finder-file-system/thumb
        [Route("thumb/{hash}")]
        public async Task<IActionResult> Thumbs(string hash)
        {
            var connector = GetConnector();
            return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
        }

        private Connector GetConnector()
        {
            // Thư mục gốc lưu trữ là wwwwroot/files (đảm bảo có tạo thư mục này)
            string pathroot = "files";

            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
            var uri = new Uri(absoluteUrl);

            // .. ... wwww/files
            string rootDirectory = Path.Combine(environment.WebRootPath, pathroot);

            // https://localhost:5001/files/
            string url = $"{uri.Scheme}://{uri.Authority}/{pathroot}/";
            string urlthumb = $"{uri.Scheme}://{uri.Authority}/file-system/thumb/";


            var root = new RootVolume(rootDirectory, url, urlthumb)
            {
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "Files", // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                //LockedFolders = new List<string>(new string[] { "Folder1" }
                ThumbnailSize = 100,
            };


            driver.AddRoot(root);

            return new Connector(driver)
            {
                MimeDetect = MimeDetectOption.Internal
            };
        }
    }
}
