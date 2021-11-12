using Ez.Magics;
using StbImageSharp;
using StbImageWriteSharp;

using RColorComponents = StbImageSharp.ColorComponents;
using WColorComponents = StbImageWriteSharp.ColorComponents;
namespace Ez.Assets.StbImageSharp
{
    public class StbImateSharpAssetWriter : IAssetWriter
    {
        public StbImateSharpAssetWriter()
        {
            MagicSupport = new MagicSupportSlim("Image", typeof(ImageResult));
        }

        public IMagicSupport MagicSupport { get; }

        public bool TryWrite(in object? value, in Type type, Stream stream)
        {
            if (value is ImageResult ir)
            {
                var writer = new ImageWriter();
                writer.WritePng(ir.Data, ir.Width, ir.Height, ToWrite(ir.Comp), stream);
                return true;
            }
            return false;
        }

        private WColorComponents ToWrite(RColorComponents cc) => cc switch
        {
            RColorComponents.Default => (WColorComponents)0,
            RColorComponents.Grey => WColorComponents.Grey,
            RColorComponents.GreyAlpha => WColorComponents.GreyAlpha,
            RColorComponents.RedGreenBlue => WColorComponents.RedGreenBlue,
            RColorComponents.RedGreenBlueAlpha => WColorComponents.RedGreenBlueAlpha,
            _ => throw new NotImplementedException(),
        };
    }
}
