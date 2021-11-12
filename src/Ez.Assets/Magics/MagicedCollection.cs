using Ez.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ez.Magics
{
    /// <summary>
    /// A collection of <see cref="IMagiced"/> with <see cref="IMagicSupport"/> implementation.
    /// </summary>
    /// <typeparam name="TMagiced">The type of elements in collection</typeparam>
    internal sealed class MagicedCollection<TMagiced> : IMagicSupport, ICollection<TMagiced> where TMagiced : IMagiced
    {
        private readonly HashSet<Type> _supported;
        private readonly HashSet<TMagiced> _references;
        private readonly MultiValueDicionary<Type, TMagiced> _links;

        /// <summary>
        /// Creates a new instance of <see cref="MagicedCollection{TMagiced}"/> class.
        /// </summary>
        /// <param name="displayName">The display name of the new instance.</param>
        public MagicedCollection(string displayName)
        {
            DisplayName = displayName;
            _supported = new HashSet<Type>();
            _references = new HashSet<TMagiced>();
            _links = new MultiValueDicionary<Type, TMagiced>();
        }

        /// <summary>
        /// Try gets a <typeparamref name="TMagiced"/> instance with <paramref name="type"/> support.
        /// </summary>
        /// <param name="type">The type expected to be supported in.</param>
        /// <param name="magiced">A <typeparamref name="TMagiced"/> that has support for type.</param>
        /// <returns><see langword="true"/> if suppoted, otherwise <see langword="false"/>.</returns>
        public bool TryGetMagiced(in Type type, out TMagiced magiced)
        {
            if (!_links.TryGetValue(type, out var collection))
            {
                magiced = default;
                return false;
            }
            magiced = collection.FirstOrDefault();
            return true;
        }

        #region IMagicSupport implementation
        /// <inheritdoc/>
        public string DisplayName { get; }

        /// <inheritdoc/>
        public IEnumerable<Type> Types => _supported;

        /// <inheritdoc/>
        public bool Supports(Type type) => _supported.Contains(type);
        #endregion

        #region ICollection<Magiced> implementation

        /// <inheritdoc/>
        public int Count => _references.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public void Add(TMagiced item)
        {
            if (_references.Add(item))
                foreach (var type in item.MagicSupport.Types)
                {
                    _links.Add(type, item);
                    _supported.Add(type);
                }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _supported.Clear();
            _references.Clear();
            _links.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(TMagiced item) => _references.Contains(item);

        /// <inheritdoc/>
        public bool Remove(TMagiced item)
        {
            if (!_references.Remove(item))
                return false;

            foreach (var type in item.MagicSupport.Types)
            {
                _links.Remove(type, item);

                if (!_links.ContainsKey(type))
                    _supported.Remove(type);
            }

            return true;
        }

        /// <inheritdoc/>
        public void CopyTo(TMagiced[] array, int arrayIndex) =>
            _references.CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public IEnumerator<TMagiced> GetEnumerator() =>
            _references.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        #endregion
    }
}
