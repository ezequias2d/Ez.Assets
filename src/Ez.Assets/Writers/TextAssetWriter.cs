using Ez.Magics;
using System;
using System.IO;

namespace Ez.Assets.Writers
{
    /// <summary>
    /// A writer for XML documents and data.
    /// </summary>
    public class TextAssetWriter : IAssetWriter
    {
        /// <summary>
        /// Creates a new instance of <see cref="TextAssetWriter"/>.
        /// </summary>
        public TextAssetWriter()
        {
            MagicSupport = new MagicSupportSlim("Text", typeof(string));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries write a text data to stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryWrite(in object value, in Type type, Stream stream)
        {
            if (type.IsAssignableFrom(typeof(string)))
            {
                using var writer = new StreamWriter(stream, leaveOpen: true);
                writer.Write((string)(object)value);
                return true;
            }
            return false;
        }
    }
}
