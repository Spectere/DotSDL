using System.Collections.Generic;

namespace Sample.Audio {
    internal static class Font {
        internal const int Width = 4;
        internal const int Height = 7;

        // Probably not particularly efficient (and definitely not elegant),
        // but this *is* just a quick sample project. :)
        internal static Dictionary<char, bool[,]> Glyph = new Dictionary<char, bool[,]> {
            {
                '0', new[,] {
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                '1', new[,] {
                    { false, false,  true, false },
                    { false,  true,  true, false },
                    { false, false,  true, false },
                    { false, false,  true, false },
                    { false, false,  true, false },
                    { false, false,  true, false },
                    { false,  true,  true,  true }
                }
            }, {
                '2', new[,] {
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    {  true,  true,  true,  true },
                    {  true, false, false, false },
                    {  true, false, false, false },
                    {  true,  true,  true,  true }
                }
            }, {
                '3', new[,] {
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                '4', new[,] {
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false, false, false,  true }
                }
            }, {
                '5', new[,] {
                    {  true,  true,  true,  true },
                    {  true, false, false, false },
                    {  true, false, false, false },
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                '6', new[,] {
                    {  true,  true,  true,  true },
                    {  true, false, false, false },
                    {  true, false, false, false },
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                '7', new[,] {
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    { false, false, false,  true }
                }
            }, {
                '8', new[,] {
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                '9', new[,] {
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true,  true,  true,  true },
                    { false, false, false,  true },
                    { false, false, false,  true },
                    {  true,  true,  true,  true }
                }
            }, {
                'h', new[,] {
                    {  true, false, false, false },
                    {  true, false, false, false },
                    {  true, false, false, false },
                    {  true,  true,  true,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true },
                    {  true, false, false,  true }
                }
            }, {
                'z', new[,] {
                    { false, false, false, false },
                    { false, false, false, false },
                    { false, false, false, false },
                    {  true,  true,  true,  true },
                    { false, false,  true, false },
                    { false,  true, false, false },
                    {  true,  true,  true,  true }
                }
            }
        };
    }
}
