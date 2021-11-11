using Ez.Magics;
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
        /// <typeparam name="T">Only can be <see cref="string"/>.</typeparam>
        /// <inheritdoc/>
        public bool TryRead<T>(Stream stream, out T value)
        {
            if (typeof(T).IsAssignableFrom(typeof(string)))
            {
                using var reader = new StreamReader(stream, leaveOpen: true);
                value = (T)(object)reader.ReadToEnd();
                return true;
            }

            value = default;
            return false;
        }
    }
}
