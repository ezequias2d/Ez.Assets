using Ez.Magics;
using StbImageSharp;

namespace Ez.Assets.StbImageSharp
{
    public class StbImageSharpAssetReader : IAssetReader
    {
        public StbImageSharpAssetReader()
        {
            MagicSupport = new MagicSupportSlim("Image", typeof(ImageResult));
        }

        public IMagicSupport MagicSupport { get; }

        public bool TryRead(in Stream stream, in Type type, out object? value)
        {
            if (type.IsAssignableFrom(typeof(ImageResult)))
            {
                value = ImageResult.FromStream(stream);
                return true;
            }

            value = null;
            return false;
        }
    }
}