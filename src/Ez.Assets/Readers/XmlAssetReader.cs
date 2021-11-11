using Ez.Magics;
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
        /// <typeparam name="T">Only can be <see cref="XmlReader"/>, <see cref="XmlDocument"/>, or <see cref="XPathDocument"/>.</typeparam>
        /// <inheritdoc/>
        public bool TryRead<T>(Stream stream, out T value)
        {
            if (typeof(T).IsAssignableFrom(typeof(XmlReader)))
            {
                value = (T)(object)XmlReader.Create(stream);
                return true;
            }

            if (typeof(T).IsAssignableFrom(typeof(XmlDocument)))
            {
                var doc = new XmlDocument();
                doc.Load(stream);
                value = (T)(object)doc;
                return true;
            }
            
            if (typeof(T).IsAssignableFrom(typeof(XPathDocument)))
            {
                value = (T)(object)new XPathDocument(stream);
                return true;
            }

            value = default;
            return false;
        }
    }
}
