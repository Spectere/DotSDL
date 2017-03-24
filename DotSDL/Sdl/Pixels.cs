using System.Diagnostics.CodeAnalysis;

namespace DotSDL.Sdl
{
    /// <summary>
    /// Contains the necessary constants and function imports from SDL_pixels.h.
    /// </summary>
    internal static class Pixels {
        internal enum PixelTypes {
            Unknown,
            Index1,
            Index4,
            Index8,
            Packed8,
            Packed16,
            Packed32,
            ArrayU8,
            ArrayU16,
            ArrayU32,
            ArrayF16,
            ArrayF32
        }

        internal enum ComponentOrder {
            None = 0x00,

            // Bitmap pixel order.
            Bitmap4321 = 0x01,
            Bitmap1234 = 0x02,

            // Packed component order.
            PackedXrgb = 0x01,
            PackedRgbx = 0x02,
            PackedArgb = 0x03,
            PackedRgba = 0x04,
            PackedXbgr = 0x05,
            PackedBgrx = 0x06,
            PackedAbgr = 0x07,
            PackedBgra = 0x08,

            // Array component order.
            ArrayRgb = 0x01,
            ArrayRgba = 0x02,
            ArrayArgb = 0x03,
            ArrayBgr = 0x04,
            ArrayBgra = 0x05,
            ArrayAbgr = 0x06
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal enum PackedComponentLayout {
            LayoutNone,
            Layout332,
            Layout4444,
            Layout1555,
            Layout5551,
            Layout565,
            Layout8888,
            Layout2101010,
            Layout1010102
        }

        internal static uint DefinePixelFormat(PixelTypes type, ComponentOrder order, PackedComponentLayout layout, byte bits, byte bytes) {
            return (uint)((1 << 28) | ((int)type << 24) | ((int)order << 20) | ((int)layout << 16) | (bits << 8) | bytes);
        }

        // Pixel formats cannot be easily expressed in an enum, so we'll make them consts instead.
        internal static readonly uint PixelFormatIndex1Lsb = DefinePixelFormat(PixelTypes.Index1, ComponentOrder.Bitmap4321, PackedComponentLayout.LayoutNone, 1, 0);
        internal static readonly uint PixelFormatIndex1Msb = DefinePixelFormat(PixelTypes.Index1, ComponentOrder.Bitmap1234, PackedComponentLayout.LayoutNone, 1, 0);
        internal static readonly uint PixelFormatIndex4Lsb = DefinePixelFormat(PixelTypes.Index4, ComponentOrder.Bitmap4321, PackedComponentLayout.LayoutNone, 4, 0);
        internal static readonly uint PixelFormatIndex4Msb = DefinePixelFormat(PixelTypes.Index4, ComponentOrder.Bitmap1234, PackedComponentLayout.LayoutNone, 4, 0);
        internal static readonly uint PixelFormatIndex8 = DefinePixelFormat(PixelTypes.Index8, ComponentOrder.None, PackedComponentLayout.LayoutNone, 8, 1);
        internal static readonly uint PixelFormatRgb332 = DefinePixelFormat(PixelTypes.Packed8, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout332, 8, 1);
        internal static readonly uint PixelFormatRgb444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout4444, 12, 2);
        internal static readonly uint PixelFormatRgb555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout1555, 15, 2);
        internal static readonly uint PixelFormatBgr555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout1555, 15, 2);
        internal static readonly uint PixelFormatArgb4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedArgb, PackedComponentLayout.Layout4444, 16, 2);
        internal static readonly uint PixelFormatRgba4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedRgba, PackedComponentLayout.Layout4444, 16, 2);
        internal static readonly uint PixelFormatAbgr4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout4444, 16, 2);
        internal static readonly uint PixelFormatBgra4444 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedBgra, PackedComponentLayout.Layout4444, 16, 2);
        internal static readonly uint PixelFormatArgb1555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedArgb, PackedComponentLayout.Layout1555, 16, 2);
        internal static readonly uint PixelFormatRgba5551 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedRgba, PackedComponentLayout.Layout5551, 16, 2);
        internal static readonly uint PixelFormatAbgr1555 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout1555, 16, 2);
        internal static readonly uint PixelFormatBgra5551 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedBgra, PackedComponentLayout.Layout5551, 16, 2);
        internal static readonly uint PixelFormatRgb565 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout565, 16, 2);
        internal static readonly uint PixelFormatBgr565 = DefinePixelFormat(PixelTypes.Packed16, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout565, 16, 2);
        internal static readonly uint PixelFormatRgb24 = DefinePixelFormat(PixelTypes.ArrayU8, ComponentOrder.ArrayRgb, PackedComponentLayout.LayoutNone, 24, 3);
        internal static readonly uint PixelFormatBgr24 = DefinePixelFormat(PixelTypes.ArrayU8, ComponentOrder.ArrayBgr, PackedComponentLayout.LayoutNone, 24, 3);
        internal static readonly uint PixelFormatRgb888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedXrgb, PackedComponentLayout.Layout8888, 24, 4);
        internal static readonly uint PixelFormatRgbx8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedRgbx, PackedComponentLayout.Layout8888, 24, 4);
        internal static readonly uint PixelFormatBgr888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedXbgr, PackedComponentLayout.Layout8888, 24, 4);
        internal static readonly uint PixelFormatBgrx8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedBgrx, PackedComponentLayout.Layout8888, 24, 4);
        internal static readonly uint PixelFormatArgb8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedArgb, PackedComponentLayout.Layout8888, 32, 4);
        internal static readonly uint PixelFormatRgba8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedRgba, PackedComponentLayout.Layout8888, 32, 4);
        internal static readonly uint PixelFormatAbgr8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedAbgr, PackedComponentLayout.Layout8888, 32, 4);
        internal static readonly uint PixelFormatBgra8888 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.PackedBgra, PackedComponentLayout.Layout8888, 32, 4);
        internal static readonly uint PixelFormatArgb2101010 = DefinePixelFormat(PixelTypes.Packed32, ComponentOrder.ArrayArgb, PackedComponentLayout.Layout2101010, 32, 4);
    }
}
