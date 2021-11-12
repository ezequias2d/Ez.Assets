using Ez.Magics;
using System;
using System.IO;

namespace Ez.Assets.Readers
{
    /// <summary>
    /// A reader for text data.
    /// </summary>
    public class TextAssetReader : IAssetReader
    {
        /// <summary>
        /// Creates a new instance of <see cref="TextAssetReader"/>.
        /// </summary>
        public TextAssetReader()
        {
            MagicSupport = new MagicSupportSlim("Text", typeof(string));
        }

        /// <inheritdoc/>
        public IMagicSupport MagicSupport { get; }

        /// <summary>
        /// Tries read a text data from stream.
        /// </summary>
        /// <inheritdoc/>
        public bool TryRead(in Stream stream, in Type type, out object value)
        {
            if (type.IsAssignableFrom(typeof(string)))
            {
                using var reader = new StreamReader(stream, leaveOpen: true);
                value = reader.ReadToEnd();
                return true;
            }

            value = default;
            return false;
        }
    }
}
