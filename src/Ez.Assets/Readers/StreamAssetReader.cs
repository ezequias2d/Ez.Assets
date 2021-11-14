using Ez.Magics;
using System;
using System.IO;

namespace Ez.Assets.Readers
{
    /// <summary>
    /// A reader for stream.
    /// </summary>
    public class StreamAssetReader : IAssetReader
    {
        /// <summary>
        /// Creates a new instance of <see cref="StreamAssetReader"/>.
        /// </summary>
        public StreamAssetReader()
        {
            MagicSupport = new MagicSupportSlim("Stream", typeof(Stream));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries read a stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryRead(in Stream stream, in Type type, out object value)
        {
            if (type.IsAssignableFrom(typeof(Stream)))
            {
                var s = new MemoryStream();
                stream.CopyTo(s);
                s.Position = 0;
                value = s;
                return true;
            }
            value = null;
            return false;
        }
    }
}
