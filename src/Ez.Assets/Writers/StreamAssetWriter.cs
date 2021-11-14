using Ez.Magics;
using System;
using System.IO;

namespace Ez.Assets.Writers
{
    /// <summary>
    /// A writer for stream.
    /// </summary>
    public class StreamAssetWriter : IAssetWriter
    {
        /// <summary>
        /// Creates a new instance of <see cref="StreamAssetWriter"/>.
        /// </summary>
        public StreamAssetWriter()
        {
            MagicSupport = new MagicSupportSlim("Stream", typeof(Stream));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries write a stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryWrite(in object value, in Type type, Stream stream)
        {
            if (type.IsAssignableFrom(typeof(Stream)))
            {
                if (value is not Stream src)
                    return false;

                src.CopyTo(stream);
                return true;
            }
            return false;
        }
    }
}
