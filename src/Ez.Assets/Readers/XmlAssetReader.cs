using Ez.Magics;
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Ez.Assets.Readers
{
    /// <summary>
    /// A reader for XML documents and data.
    /// </summary>
    public class XmlAssetReader : IAssetReader
    {
        /// <summary>
        /// Creates a new instance of <see cref="XmlAssetReader"/>.
        /// </summary>
        public XmlAssetReader()
        {
            MagicSupport = new MagicSupportSlim("XML Document", typeof(XmlReader), typeof(XmlDocument), typeof(XPathDocument));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries read a XML document from stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryRead(in Stream stream, in Type type, out object value)
        {
            if (type.IsAssignableFrom(typeof(XmlReader)))
            {
                value = XmlReader.Create(stream);
                return true;
            }

            if (type.IsAssignableFrom(typeof(XmlDocument)))
            {
                var doc = new XmlDocument();
                doc.Load(stream);
                value = doc;
                return true;
            }
            
            if (type.IsAssignableFrom(typeof(XPathDocument)))
            {
                value = new XPathDocument(stream);
                return true;
            }

            value = default;
            return false;
        }
    }
}
