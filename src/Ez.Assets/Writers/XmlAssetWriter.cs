using Ez.Magics;
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Ez.Assets.Writers
{
    /// <summary>
    /// A writer for XML documents and data.
    /// </summary>
    public class XmlAssetWriter : IAssetWriter
    {
        /// <summary>
        /// Creates a new instance of <see cref="XmlAssetWriter"/>.
        /// </summary>
        public XmlAssetWriter()
        {
            MagicSupport = new MagicSupportSlim("XML Document", typeof(XmlReader), typeof(XmlDocument), typeof(XPathDocument));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries write a XML document to stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryWrite(in object value, in Type type, Stream stream)
        {
            if (type.IsAssignableFrom(typeof(XmlWriter)))
            {
                var reader = (XmlReader)(object)value;
                using var writer = XmlWriter.Create(stream);
                writer.WriteNode(reader, true);
                return true;
            }

            if (type.IsAssignableFrom(typeof(XmlDocument)))
            {
                var doc = (XmlDocument)(object)value;
                doc.Save(stream);
                return true;
            }

            if (type.IsAssignableFrom(typeof(XPathDocument)))
            {
                var xdoc = (XPathDocument)(object)value;
                using var writer = XmlWriter.Create(stream);
                var navigator = xdoc.CreateNavigator();
                writer.WriteNode(navigator, true);
                return true;
            }
            return false;
        }
    }
}
