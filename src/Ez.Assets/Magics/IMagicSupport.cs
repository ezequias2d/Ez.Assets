using System;
using System.Collections.Generic;

namespace Ez.Magics
{
    /// <summary>
    /// Provides type support verification 
    /// </summary>
    public interface IMagicSupport
    {
        /// <summary>
        /// Gets the display name of a <see cref="IMagicSupport"/>.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Checks if a type is compatible.
        /// </summary>
        /// <typeparam name="T">The type to be checked.</typeparam>
        /// <returns><see langword="true"/> if suppoted, otherwise 
        /// <see langword="false"/>.</returns>
        public bool Supports<T>() => Supports(typeof(T));

        /// <summary>
        /// Checks if a type is compatible.
        /// </summary>
        /// <param name="type">The type to be checked.</param>
        /// <returns><see langword="true"/> if suppoted, otherwise 
        /// <see langword="false"/>.</returns>
        public bool Supports(Type type);

        /// <summary>
        /// Gets supported types.
        /// </summary>
        IEnumerable<Type> Types { get; }
    }
}
