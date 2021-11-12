using System;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// A IAssetSource using common files.
    /// </summary>
    public class FileAssetSource : IAssetSource
    {
        private readonly IAssetReader _reader;
        private readonly IAssetWriter _writer;

        /// <summary>
        /// Creates a new instance of <see cref="FileAssetSource"/>;
        /// </summary>
        /// <param name="reader">The asset reader.</param>
        /// <param name="writer">The asset writer.</param>
        public FileAssetSource(IAssetReader reader, IAssetWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        /// <inheritdoc/>
        public bool ReadAsset(in string assetName, in Type type, out object asset)
        {
            using var file = File.OpenRead(assetName);
            return _reader.TryRead(file, type, out asset);
        }

        /// <inheritdoc/>
        public bool WriteAsset(in object asset, in string assetName, in Type type)
        {
            using var file = File.Create(assetName);
            return _writer.TryWrite(asset, type, file);
        }
    }
}
