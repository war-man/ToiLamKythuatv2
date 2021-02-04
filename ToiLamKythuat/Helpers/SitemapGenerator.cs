using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace ToiLamKythuat.Helpers
{
    public class SitemapGenerator
    {
        XmlTextWriter writer;

        public SitemapGenerator(System.IO.Stream stream, System.Text.Encoding encoding)
        {
            writer = new XmlTextWriter(stream, encoding);
            writer.Formatting = Formatting.Indented;
        }

        public SitemapGenerator(System.IO.TextWriter w)
        {
            writer = new XmlTextWriter(w);
            writer.Formatting = Formatting.Indented;
        }
        /// <summary>
        /// Writes the beginning of the SiteMap document
        /// </summary>
        public void WriteStartDocument()
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");

            writer.WriteAttributeString("xmlns", "http://www.google.com/schemas/sitemap/0.84");
        }

        /// <summary>
        /// Writes the end of the SiteMap document
        /// </summary>
        public void WriteEndDocument()
        {
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }

        /// <summary>
        /// Closes this stream and the underlying stream
        /// </summary>
        public void Close()
        {
            writer.Flush();
            writer.Close();
        }

        public void WriteItem(string link, DateTime publishedDate, string priority)
        {
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", link);
            writer.WriteElementString("lastmod", publishedDate.ToString("yyyy-MM-dd"));
            writer.WriteElementString("changefreq", "always");
            writer.WriteElementString("priority", priority);
            writer.WriteEndElement();
        }
    }
}
