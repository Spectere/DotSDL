using System;
using System.Runtime.InteropServices;
using DotSDL.Graphics;

namespace DotSDL.Interop.Core {
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_render.h.
    /// </summary>
    internal static class Render {
        /// <summary>
        /// Flags used when creating a rendering context.
        /// </summary>
        [Flags]
        internal enum RendererFlags : uint {
            /// <summary>The renderer is a software fallback.</summary>
            Software = 0x00000001,

            /// <summary>The renderer uses hardware acceleration.</summary>
            Accelerated = 0x00000002,

            /// <summary>Present is synchronized with the refresh rate.</summary>
            PresentVsync = 0x00000004,

            /// <summary>The renderer supports rendering to a texture.</summary>
            TargetTexture = 0x00000008
        }

        /// <summary>
        /// The access pattern allowed for a texture.
        /// </summary>
        internal enum TextureAccess : int {
            /// <summary>Changes rarely, not lockable.</summary>
            Static,

            /// <summary>Changes frequently, lockable.</summary>
            Streaming,

            /// <summary>Texture can be used as a render target.</summary>
            Target
        }

        /// <summary>
        /// Create a 2D rendering context for a window.
        /// </summary>
        /// <param name="window">The window whwere rendering is displayed.</param>
        /// <param name="index">The index of the rendering driver to initialize, or -1
        /// to initialize the first one supporting the requested flags.</param>
        /// <param name="flags">The requested flags for the renderer.</param>
        /// <returns>A valid rendering context or NULL if there was an error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateRenderer(IntPtr window, int index, RendererFlags flags);

        /// <summary>
        /// Create a texture for a rendering context.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="format">The format of the texture.</param>
        /// <param name="access">The access pattern for the new texture.</param>
        /// <param name="w">The width of the texture in pixels.</param>
        /// <param name="h">The height of the texture in pixels.</param>
        /// <returns>The created texture is returned, or NULL if no rendering context was
        /// active, the foramt was unsupported, or the width or height were out of range.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_CreateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateTexture(IntPtr renderer, uint format, TextureAccess access, int w, int h);

        /// <summary>
        /// Clear the current rendering target with the drawing color.
        ///
        /// This function clears the entire rendering target, ignoring the viewport and the clip rectangle.
        /// </summary>
        /// <param name="renderer">The renderer to clear.</param>
        /// <returns>0 on success, -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderClear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderClear(IntPtr renderer);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopy(IntPtr renderer, IntPtr texture, Rect.SdlRect srcRect, Rect.SdlRect dstRect);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <param name="angle">An angle, in degrees, that indicates the rotation that will be applied to <paramref name="dstRect"/>.</param>
        /// <param name="center">An <see cref="Rect.SdlPoint"/> indicating the point around which <paramref name="dstRect"/> will be rotated
        /// (if NULL, rotation will be done around dstRect.w/2, dstRect.h/2).</param>
        /// <param name="flip">A <see cref="RendererFlip"/> value indicating which flipping actions should be performed.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, Rect.SdlRect srcRect, Rect.SdlRect dstRect, double angle, Rect.SdlPoint center, FlipDirection flip);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <param name="angle">An angle, in degrees, that indicates the rotation that will be applied to <paramref name="dstRect"/>.</param>
        /// <param name="center">An <see cref="Rect.SdlPoint"/> indicating the point around which <paramref name="dstRect"/> will be rotated
        /// (if NULL, rotation will be done around dstRect.w/2, dstRect.h/2).</param>
        /// <param name="flip">A <see cref="RendererFlip"/> value indicating which flipping actions should be performed.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect, double angle, Rect.SdlPoint center, FlipDirection flip);

        /// <summary>
        /// Update the screen with the rendering performed.
        /// </summary>
        /// <param name="renderer">The renderer to update.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderPresent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RenderPresent(IntPtr renderer);

        /// <summary>
        /// Update the given texture rectangle with new pixel data.
        /// </summary>
        /// <param name="texture">The texture to update.</param>
        /// <param name="rect">A rectangle of pixels to update, or NULL to update the entire texture.</param>
        /// <param name="pixels">The raw pixel data.</param>
        /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
        /// <returns>0 on success, or -1 if the texture is not valid.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_UpdateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

        /// <summary>
        /// Update the given texture rectangle with new pixel data.
        /// </summary>
        /// <param name="texture">The texture to update.</param>
        /// <param name="rect">A rectangle of pixels to update, or NULL to update the entire texture.</param>
        /// <param name="pixels">The raw pixel data.</param>
        /// <param name="pitch">The number of bytes in a row of pixel data, including padding between lines.</param>
        /// <returns>0 on success, or -1 if the texture is not valid.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_UpdateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int UpdateTexture(IntPtr texture, Rect.SdlRect rect, IntPtr pixels, int pitch);
    }
}
