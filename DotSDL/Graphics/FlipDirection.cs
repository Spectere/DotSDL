using System;

namespace DotSDL.Graphics {
    /// <summary>
    /// The axis upon which a sprite should be flipped.
    /// </summary>
    [Flags]
    public enum FlipDirection {
        /// <summary>
        /// The sprite should not be flipped.
        /// </summary>
        None = 0x00,
        
        /// <summary>
        /// The sprite should be flipped along the X (horizontal) axis.
        /// </summary>
        Horizontal = 0x01,
        
        /// <summary>
        /// The sprite should be flipped along the Y (vertical) axis.
        /// </summary>
        Vertical = 0x02
    }
}
