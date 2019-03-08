using System;
using System.Collections;
using System.Collections.Generic;
using DotSDL.Interop.Core;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a collection of <see cref="Sprite"/> objects.
    /// </summary>
    public class SpriteList : IList<Sprite> {
        private readonly IntPtr _renderer;
        private readonly List<Sprite> _sprites = new List<Sprite>();

        /// <summary>Retrieves the number of <see cref="Sprite"/> objects contained in this <see cref="SpriteList"/>.</summary>
        public int Count => _sprites.Count;

        /// <summary>Indicates whether or not this <see cref="SpriteList"/> is read-only.</summary>
        public bool IsReadOnly => false;

        /// <inheritdoc/>
        public Sprite this[int index] {
            get => _sprites[index];
            set {
                DestroySprite(_sprites[index]);
                InitializeSprite(value);
                _sprites[index] = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="SpriteList"/>.
        /// </summary>
        /// <param name="renderer">The SDL2 rendering context that should be used.</param>
        public SpriteList(IntPtr renderer) {
            _renderer = renderer;
        }

        /// <summary>
        /// Adds a <see cref="Sprite"/> to this <see cref="SpriteList"/>.
        /// </summary>
        /// <param name="item">The <see cref="Sprite"/> to add.</param>
        public void Add(Sprite item) {
            InitializeSprite(item);
            _sprites.Add(item);
        }

        /// <summary>
        /// Removes all items from this <see cref="SpriteList"/>.
        /// </summary>
        public void Clear() {
            _sprites.ForEach(DestroySprite);
            _sprites.Clear();
        }

        /// <summary>
        /// Determines whether this <see cref="SpriteList"/> contains a particular instance
        /// of a <see cref="Sprite"/>.
        /// </summary>
        /// <param name="item">The <see cref="Sprite"/> instance to look for.</param>
        /// <returns><c>true</c> if the <see cref="Sprite"/> instance in <paramref name="item"/> was found, otherwise <c>false</c>.</returns>
        public bool Contains(Sprite item) => _sprites.Contains(item);

        /// <summary>
        /// Copies the eleents on this <see cref="SpriteList"/> into an array, starting at
        /// index <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">The array to copy this <see cref="SpriteList"/> into.</param>
        /// <param name="arrayIndex">The intex to start copying from.</param>
        public void CopyTo(Sprite[] array, int arrayIndex) => _sprites.CopyTo(array, arrayIndex);

        /// <summary>
        /// Frees the texture used by a sprite prior to removing it from the list.
        /// </summary>
        /// <param name="sprite">The <see cref="Sprite"/> to destroy.</param>
        private void DestroySprite(Canvas sprite) {
            Render.DestroyTexture(sprite.Texture);
        }

        /// <inheritdoc/>
        public IEnumerator<Sprite> GetEnumerator() => _sprites.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Creates a texture for a sprite prior to adding it to the list.
        /// </summary>
        /// <param name="sprite"></param>
        private void InitializeSprite(Canvas sprite) {
            sprite.Texture = Render.CreateTexture(_renderer, Pixels.PixelFormatArgb8888, Render.TextureAccess.Static,
                                                   sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Removes a <see cref="Sprite"/> from this <see cref="SpriteList"/>.
        /// </summary>
        /// <param name="item">The <see cref="Sprite"/> to remove.</param>
        /// <returns><c>true</c> if the <see cref="Sprite"/> was successfully removed, otherwise <c>false</c>.</returns>
        public bool Remove(Sprite item) {
            DestroySprite(item);
            return _sprites.Remove(item);
        }

        /// <summary>
        /// Determines the index of a specific <see cref="Sprite"/> ionstnace in the <see cref="SpriteList"/>.
        /// </summary>
        /// <param name="item">The <see cref="Sprite"/> instance to locate.</param>
        /// <returns>The index of the <see cref="Sprite"/> instance referenced by <paramref name="item"/> if it's been found in the list, otherwise -1.</returns>
        public int IndexOf(Sprite item) => _sprites.IndexOf(item);

        /// <summary>
        /// Inserts a <see cref="Sprite"/> into the <see cref="SpriteList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The <see cref="Sprite"/> to insert into the list.</param>
        public void Insert(int index, Sprite item) {
            InitializeSprite(item);
            _sprites.Insert(index, item);
        }

        /// <summary>
        /// Removes the <see cref="Sprite"/> contained at the specified index from this <see cref="SpriteList"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="Sprite"/> to remove.</param>
        public void RemoveAt(int index) {
            DestroySprite(_sprites[index]);
            _sprites.RemoveAt(index);
        }
    }
}
