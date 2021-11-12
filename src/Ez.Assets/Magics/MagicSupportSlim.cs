using System;
using System.Collections.Generic;

namespace Ez.Magics
{
    /// <summary>
    /// A lightweight implementation of <see cref="IMagicSupport"/>.
    /// </summary>
    public sealed class MagicSupportSlim : IMagicSupport
    {
        private readonly HashSet<Type> _types;

        /// <summary>
        /// Creates a new instance of <see cref="MagicSupportSlim"/> class.
        /// </summary>
        /// <param name="displayName">The value of <see cref="DisplayName"/>.</param>
        /// <param name="types">Supported types in <see cref="Types"/> and <see cref="Supports(Type)"/>.</param>
        public MagicSupportSlim(string displayName, params Type[] types)
        {
            DisplayName = displayName;
            _types = new HashSet<Type>(types);
        }

        /// <inheritdoc/>
        public string DisplayName { get; }

        /// <inheritdoc/>
        public IEnumerable<Type> Types => _types;

        /// <inheritdoc/>
        public bool Supports(Type type) => _types.Contains(type);
    }
}
