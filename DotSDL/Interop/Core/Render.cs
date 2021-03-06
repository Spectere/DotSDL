﻿using System;
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
        internal enum TextureAccess {
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
        /// Destroys a rendering context for a window and frees all associated textures.
        /// </summary>
        /// <param name="texture">The rendering context to destroy.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_DestroyRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DestroyRenderer(IntPtr texture);

        /// <summary>
        /// Destroys a texture.
        /// </summary>
        /// <param name="texture">The texture to destroy.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_DestroyTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DestroyTexture(IntPtr texture);

        /// <summary>
        /// Locks a portion of a texture for write-only pixel access.
        /// </summary>
        /// <param name="texture">The texture to lock for access. This texture must have been created with <see cref="TextureAccess.Streaming"/>.</param>
        /// <param name="rect">An <see cref="Rect.SdlRect"/> structure representing the area to lock for access, or <see cref="IntPtr.Zero"/> to lock the entire texture.</param>
        /// <param name="pixels">A pointer to the locked pixels, offset by the locked area.</param>
        /// <param name="pitch">The pitch of the locked pixels. The pitch is the length of one row in bytes.</param>
        /// <returns>0 on success or a negative error code if the texture is not valid or was not created with <see cref="TextureAccess.Streaming"/>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int LockTexture(IntPtr texture, IntPtr rect, out void* pixels, out int pitch);

        /// <summary>
        /// Locks a portion of a texture for write-only pixel access.
        /// </summary>
        /// <param name="texture">The texture to lock for access. This texture must have been created with <see cref="TextureAccess.Streaming"/>.</param>
        /// <param name="rect">An <see cref="Rect.SdlRect"/> structure representing the area to lock for access, or <see cref="IntPtr.Zero"/> to lock the entire texture.</param>
        /// <param name="pixels">A pointer to the locked pixels, offset by the locked area.</param>
        /// <param name="pitch">The pitch of the locked pixels. The pitch is the length of one row in bytes.</param>
        /// <returns>0 on success or a negative error code if the texture is not valid or was not created with <see cref="TextureAccess.Streaming"/>.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_LockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe int LockTexture(IntPtr texture, Rect.SdlRect rect, out void* pixels, out int pitch);

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
        /// <param name="center">An <see cref="IntPtr"/> referencing an <see cref="Rect.SdlPoint"/> around which <paramref name="dstRect"/>
        /// will be rotated (if NULL, rotation will be done around dstRect.w/2, dstRect.h/2).</param>
        /// <param name="flip">A <see cref="FlipDirection"/> value indicating which flipping actions should be performed.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, Rect.SdlRect srcRect, Rect.SdlRect dstRect, double angle, IntPtr center, FlipDirection flip);

        /// <summary>
        /// Copy a portion of the texture to the current rendering target.
        /// </summary>
        /// <param name="renderer">The renderer which should copy parts of a texture.</param>
        /// <param name="texture">The source texture.</param>
        /// <param name="srcRect">The source rectangle, or NULL for the entire texture.</param>
        /// <param name="dstRect">The destination rectangle, or NULL For the entire rendering target.</param>
        /// <param name="angle">An angle, in degrees, that indicates the rotation that will be applied to <paramref name="dstRect"/>.</param>
        /// <param name="center">An <see cref="IntPtr"/> referencing an <see cref="Rect.SdlPoint"/> around which <paramref name="dstRect"/>
        /// will be rotated (if NULL, rotation will be done around dstRect.w/2, dstRect.h/2).</param>
        /// <param name="flip">A <see cref="FlipDirection"/> value indicating which flipping actions should be performed.</param>
        /// <returns>0 on success, or -1 on error.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderCopyEx", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect, double angle, IntPtr center, FlipDirection flip);

        /// <summary>
        /// Update the screen with the rendering performed.
        /// </summary>
        /// <param name="renderer">The renderer to update.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_RenderPresent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RenderPresent(IntPtr renderer);

        /// <summary>
        /// Sets an additional alpha value that will be multiplied into render copy operations.
        /// </summary>
        /// <param name="texture">The texture to update.</param>
        /// <param name="alpha">The source alpha value multiplied into copy operations.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetTextureAlphaMod", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetTextureAlphaMod(IntPtr texture, byte alpha);

        /// <summary>
        /// Modulates the color of a texture when it is used in a render copy operation.
        /// </summary>
        /// <param name="texture">The texture to update.</param>
        /// <param name="r">The red color value multiplied into copy operations.</param>
        /// <param name="g">The green color value multiplied into copy operations.</param>
        /// <param name="b">The blue color value multiplied into copy operations.</param>
        /// <returns>0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetTextureColorMod", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

        /// <summary>
        /// Sets a texture as the current rendering target.
        /// </summary>
        /// <param name="renderer">The rendering context.</param>
        /// <param name="texture">The targeted texture, or <see cref="IntPtr.Zero"/> for the default render target.
        /// If a texture is used, it must have been created with the <see cref="TextureAccess.Target"/> flag.</param>
        /// <returns>Returns 0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetRenderTarget", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetRenderTarget(IntPtr renderer, IntPtr texture);

        /// <summary>
        /// Sets the blend mode for a texture, used by <see cref="RenderCopy(System.IntPtr,System.IntPtr,DotSDL.Interop.Core.Rect.SdlRect,DotSDL.Interop.Core.Rect.SdlRect)"/>.
        /// </summary>
        /// <param name="texture">The texture to update.</param>
        /// <param name="blendMode">The <see cref="BlendMode"/> to use for texture blending.</param>
        /// <returns>Returns 0 on success or a negative error code on failure.</returns>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_SetTextureBlendMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int SetTextureBlendMode(IntPtr texture, BlendMode blendMode);

        /// <summary>
        /// Unlocks a texture, uploading the changes to video memory if needed.
        /// </summary>
        /// <param name="texture">A locked texture.</param>
        [DllImport(Meta.CoreLib, EntryPoint = "SDL_UnlockTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void UnlockTexture(IntPtr texture);

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
