using System;

namespace Ez.Assets
{
    /// <summary>
    /// Represents a source from which assets can be read and written.
    /// </summary>
    public interface IAssetSource
    {
        /// <summary>
        /// Writes an <paramref name="asset"/> in the <see cref="IAssetSource"/>.
        /// </summary>
        /// <typeparam name="T">The type of asset to be written.</typeparam>
        /// <param name="asset">The asset to be written.</param>
        /// <param name="assetName">The name of asset to be written.</param>
        public void WriteAsset<T>(in T asset, in string assetName) => WriteAsset(asset, assetName, typeof(T));

        /// <summary>
        /// Reads an asset from the <see cref="IAssetSource"/>.
        /// </summary>
        /// <typeparam name="T">The type of asset to be read.</typeparam>
        /// <param name="assetName">The name of asset to be read.</param>
        /// <param name="asset">The asset read from the source, otherwise, <see langword="default"/>.</param>
        /// <returns><see langword="true"/>, if the asset was read correctly, otherwise <see langword="false"/>.</returns>
        public bool ReadAsset<T>(in string assetName, out T asset) 
        { 
            var result = ReadAsset(assetName, typeof(T), out var tmp);
            asset = (T)tmp;
            return result;
        }

        /// <summary>
        /// Writes an <paramref name="asset"/> in the <see cref="IAssetSource"/>.
        /// </summary>
        /// <param name="asset">The asset to be written.</param>
        /// <param name="assetName">The name of asset to be written.</param>
        public void WriteAsset(in object asset, in string assetName) => WriteAsset(asset, assetName, asset.GetType());

        /// <summary>
        /// Writes an <paramref name="asset"/> in the <see cref="IAssetSource"/>.
        /// </summary>
        /// <param name="asset">The asset to be written.</param>
        /// <param name="assetName">The name of asset to be written.</param>
        /// <param name="type">The type of asset to be written.</param>
        /// <returns><see langword="true"/>, if the asset was write correctly, otherwise <see langword="false"/>.</returns>
        public bool WriteAsset(in object asset, in string assetName, in Type type);

        /// <summary>
        /// Reads an asset from the <see cref="IAssetSource"/>.
        /// </summary>
        /// <param name="assetName">The name of asset to be read.</param>
        /// <param name="type">The type of asset to be read.</param>
        /// <param name="asset">The asset read from the source, otherwise, <see langword="default"/>.</param>
        /// <returns><see langword="true"/>, if the asset was read correctly, otherwise <see langword="false"/>.</returns>
        public bool ReadAsset(in string assetName, in Type type, out object asset);
    }
}
