using DotSDL.Interop.Core;
using System;

namespace DotSDL.Graphics {
    public class Background : Canvas {
        /// <summary>
        /// Initializes a new <see cref="Background"/>.
        /// </summary>
        /// <param name="textureWidth">The width of the new <see cref="Background"/>.</param>
        /// <param name="textureHeight">The height of the new <see cref="Background"/>.</param>
        public Background(int textureWidth, int textureHeight) : base(textureWidth, textureHeight) { }

        /// <summary>
        /// Initializes a new <see cref="Background"/>.
        /// </summary>
        /// <param name="textureWidth">The width of the new <see cref="Background"/>.</param>
        /// <param name="textureHeight">The height of the new <see cref="Background"/>.</param>
        /// <param name="clipping">A rectangle specifying which part of this <see cref="Background"/> should be drawn.</param>
        public Background(int textureWidth, int textureHeight, Rectangle clipping) : base(textureWidth, textureHeight, clipping) { }

        /// <inheritdoc/>
        internal override void CreateTexture() {
            CreateTexture(Render.TextureAccess.Streaming);
        }

        /// <inheritdoc/>
        internal override bool UpdateTexture() {
            if(!HasTexture) return false;

            // TODO: Might be able to make this faster by leveraging memcpy(). Need to benchmark it.
            unsafe {
                var copySize = Width * Height * 4;  // Width x Height at 4 bytes per pixel.
                Render.LockTexture(Texture, IntPtr.Zero, out var pixels, out var _);
                Buffer.MemoryCopy(GetCanvasPointer().ToPointer(), pixels, copySize, copySize);
                Render.UnlockTexture(Texture);
            }

            return true;
        }
    }
}
