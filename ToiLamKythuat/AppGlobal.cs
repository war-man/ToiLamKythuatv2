using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToiLamKythuat
{
    public class AppGlobal
    {
        public static string ConnectionString
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("ConnectionStrings")
                    .GetSection("ConnectionString").Value;
            }
        }

        public static string ContentLanguage
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("ContentLanguage").Value;
            }
        }

        public static string SiteName
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("SiteName").Value;
            }
        }
        
        public static string SiteUrl
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("SiteUrl").Value;
            }
        }

        public static string SiteDescription
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("SiteDescription").Value;
            }
        }

        public static string SiteImage
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("SiteImage").Value;
            }
        }
        public static List<string> SiteKeywords
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("GlobalConfig")
                    .GetSection("SiteKeywords").Get<List<string>>();
            }
        }
    }
}
