using System;

namespace Ez.Assets
{
    /// <summary>
    /// Represents a content manager that provides an interface for acquiring assets.
    /// </summary>
    public interface IAssetManager : IDisposable
    {
        /// <summary>
        /// Gets the first loaded asset, if it is already loaded, 
        /// otherwise try load the asset.
        /// </summary>
        /// <typeparam name="T">The asset type to be get.</typeparam>
        /// <param name="assetName">The asset name to get.</param>
        /// <returns>The first asset found that matches the type 
        /// and name, otherwise <see langword="null"/></returns>
        public T GetAsset<T>(string assetName) => (T)GetAsset(assetName, typeof(T));

        /// <summary>
        /// Gets the first loaded asset, if it is already loaded, 
        /// otherwise try load the asset.
        /// </summary>
        /// <param name="type">The asset type to be get.</param>
        /// <param name="assetName">The asset name to get.</param>
        /// <returns>The first asset found that matches the type 
        /// and name, otherwise <see langword="null"/></returns>
        object GetAsset(string assetName, Type type);

        /// <summary>
        /// Loads an asset to the content manager.
        /// </summary>
        /// <param name="assetName">The name of the asset to be loaded.</param>
        /// <param name="asset">The asset to be loaded.</param>
        void LoadAsset(string assetName, in object asset);

        /// <summary>
        /// Unloads an asset by type and name in a <see cref="IAssetManager"/> instance.
        /// </summary>
        /// <param name="assetName">The name of the asset to be unloaded.</param>
        /// <param name="type">The asset type to be unloaded.</param>
        void UnloadAsset(string assetName, Type type);

        /// <summary>
        /// Unloads all assets in a <see cref="IAssetManager"/> instance.
        /// </summary>
        void Unload();
    }
}
