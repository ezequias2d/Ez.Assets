using Ez.IO.Archives;
using System.Diagnostics;

namespace Ez.Assets.Archive
{
    public class ArchiveAssetSource : IAssetSource
    {
        private readonly IAssetReader _reader;
        private readonly IAssetWriter _writer;
        private readonly IArchive _archive;
        private IDictionary<string, string> _links;

        public ArchiveAssetSource(IArchive archive, IAssetReader reader, IAssetWriter writer)
        {
            _reader = reader;
            _writer = writer;
            _archive = archive;

            _links = new Dictionary<string, string>();

            foreach(var file in _archive.EntryNames)
            {
                _links.Add(Path.ChangeExtension(file, null), file);
            }
        }

        public bool ReadAsset(in string assetName, in Type type, out object? asset)
        {
            if(_links.TryGetValue(assetName, out var link))
            {
                try
                {
                    var entry = _archive.GetEntry(link);
                    using var stream = entry.Open();
                    return _reader.TryRead(stream, out asset);
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }

            asset = null;
            return false;
        }

        /// <inheritdoc/>
        public bool WriteAsset(in object asset, in string assetName, in Type type)
        {
            var link = _links[assetName] = Path.ChangeExtension(assetName, ".asset");
            
            var entry = _archive.GetEntry(link);
            if(entry != null)
            {
                using var stream = entry.Open();
                var result = _writer.TryWrite(asset, type, stream);
                stream.SetLength(stream.Position);
                return result;
            }

            entry = _archive.CreateEntry(link);
            {
                using var stream = entry.Open();
                return _writer.TryWrite(asset, type, stream);
            }
        }
    }
}
