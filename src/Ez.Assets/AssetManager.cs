using System;
using System.Collections.Concurrent;
using Ez.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Ez.Assets
{
    /// <summary>
    /// Represents that implementation a cached <see cref="IAssetManager"/>.
    /// </summary>
    public sealed class AssetManager : IAssetManager
    {
        #region fields
        private readonly MultiValueDicionary<string, object> _loadedAssets;
        private readonly ConcurrentList<IDisposable> _disposableAssets;
        private readonly IAssetSource _assetSource;
        private bool _disposed;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetManager"/> class 
        /// with a <see cref="IAssetSource"/>.
        /// </summary>
        /// <param name="assetSource"></param>
        public AssetManager(IAssetSource assetSource)
        {
            if (assetSource == null)
                throw new NullReferenceException(nameof(assetSource));

            _assetSource = assetSource;
            _loadedAssets = new MultiValueDicionary<string, object>(
                new ConcurrentDictionary<string, ICollection<object>>(StringComparer.OrdinalIgnoreCase), 
                () => new ConcurrentList<object>());
            _disposableAssets = new ConcurrentList<IDisposable>();
            _disposed = false;
        }

        /// <summary>
        /// Destroys a instance of <see cref="AssetManager"/> class.
        /// </summary>
        ~AssetManager() => Dispose(false);

        #region public methods

        /// <inheritdoc/>
        public void Dispose() => Dispose(true);

        /// <inheritdoc/>
        public void Unload()
        {
            using(var operation = _disposableAssets.GetOperationList())
            {
                foreach(var disposable in operation)
                {
                    disposable.Dispose();
                }
                operation.Clear();
            }
            _loadedAssets.Clear();
        }

        /// <inheritdoc/>
        public object GetAsset(string assetName, Type type)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(AssetManager));

            if (string.IsNullOrEmpty(assetName))
                throw new ArgumentNullException(nameof(assetName));

            // Check for a previously loaded asset first
            if (TryGetAsset(assetName, type, out var asset))
                return asset;

            // Read the asset.
            if (_assetSource.ReadAsset(assetName, type, out asset))
                LoadAsset(assetName, asset);
            else
                throw new ArgumentException($"The asset {assetName} was not found");

            return asset;
        }

        /// <inheritdoc/>
        public void LoadAsset(string assetName, in object asset)
        {
            _loadedAssets.Add(assetName, asset);
            if (asset is IDisposable disposable)
                _disposableAssets.Add(disposable);
        }

        /// <inheritdoc/>
        public void UnloadAsset(string assetName, Type type)
        {
            if (TryGetAsset(assetName, type, out var asset))
            {
                _loadedAssets.Remove(assetName, asset);

                if (asset is IDisposable disposable)
                {
                    _disposableAssets.Remove(disposable);
                    disposable.Dispose();
                }
            }
        }
        #endregion

        #region private methods
        private void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                _disposed = true;

                if (disposing)
                    Unload();
            }
        }

        private bool TryGetAsset(string assetName, Type type, out object asset)
        {

            if (_loadedAssets.TryGetValue(assetName, out IReadOnlyCollection<object> objs))
            {
                asset = objs.First((element) => type.IsAssignableFrom(element.GetType()));
                return true;
            }
            asset = default;
            return false;
        }
        #endregion
    }
}
