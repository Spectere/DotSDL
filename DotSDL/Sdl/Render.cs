using System;
using System.Runtime.InteropServices;

namespace DotSDL.Sdl {
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
        [DllImport(Meta.DllName, EntryPoint = "SDL_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
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
        [DllImport(Meta.DllName, EntryPoint = "SDL_CreateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CreateTexture(IntPtr renderer, uint format, TextureAccess access, int w, int h);

        /// <summary>
        /// Lock a portion of the texture for write-only pixel access.
        /// </summary>
        /// <param name="texture">The texture to lock for access, which was created with <see cref="TextureAccess.Streaming"/>.</param>
        /// <param name="rect">The rectangle to lock for access. If the rect is NULL, the entire texture will be locked.</param>
        /// <param name="pixels">This is filled in with an <see cref="IntPtr"/> to the locked pixels, appropriately offset
        /// by the locked area.</param>
        /// <param name="pitch">This is filled in with the pitch of the locked pixels.</param>
        /// <returns>0 on success, or -1 if the texture is not valid or was not created with <see cref="TextureAccess.Streaming"/>.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LockTexture(IntPtr texture, Rect.SdlRect rect, out IntPtr pixels, out int pitch);
        [DllImport(Meta.DllName, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);

        /// <summary>
        /// Clear the current rendering target with the drawing color.
        /// 
        /// This function clears the entire rendering target, ignoring the viewport and the clip rectangle.
        /// </summary>
        /// <param name="renderer">The renderer to clear.</param>
        /// <returns>0 on success, -1 on error.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_RenderClear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderClear(IntPtr renderer);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.DllName, EntryPoint = "SDL_RenderCopy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopy(IntPtr renderer, IntPtr texture, Rect.SdlRect srcRect, Rect.SdlRect dstRect);
        [DllImport(Meta.DllName, EntryPoint = "SDL_RenderCopy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect);

        /// <summary>
        /// Update the screen with the rendering performed.
        /// </summary>
        /// <param name="renderer">The renderer to update.</param>
        [DllImport(Meta.DllName, EntryPoint = "SDL_RenderPresent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RenderPresent(IntPtr renderer);

        /// <summary>
        /// Unlock a texture, uploading the changes to video memory, if needed.
        /// </summary>
        /// <param name="texture">The texture to unlock.</param>
        [DllImport(Meta.DllName, EntryPoint = "SDL_UnlockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UnlockTexture(IntPtr texture);
    }
}
