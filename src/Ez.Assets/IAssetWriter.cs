using Ez.Magics;
using System;
using System.IO;

namespace Ez.Assets
{
    /// <summary>
    /// Provides an interface to serialize.
    /// </summary>
    public interface IAssetWriter : IMagiced
    {
        /// <summary>
        /// Attempts to write an <paramref name="value"/> to the stream.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="value">The instance to be written.</param>
        /// <param name="stream">The destination stream.</param>
        /// <returns></returns>
        public bool TryWrite<T>(in T value, Stream stream) => TryWrite(value, typeof(T), stream);

        /// <summary>
        /// Attempts to write an <paramref name="value"/> to the stream.
        /// </summary>
        /// <param name="type">The type of <paramref name="value"/>.</param>
        /// <param name="value">The instance to be written.</param>
        /// <param name="stream">The destination stream.</param>
        /// <returns></returns>
        public bool TryWrite(in object value, in Type type, Stream stream);
    }
}
