using DotSDL.Interop.Core;
using DotSDL.Math.Vector;
using SdlPixels = DotSDL.Interop.Core.Pixels;

namespace DotSDL.Graphics {
    /// <summary>
    /// Represents a graphical, two-dimensional object in a program.
    /// </summary>
    public class Sprite : Canvas {
        private readonly Point _effectiveSize = new Point();
        private Vector2<float> _scale;
        private bool _collisionBoxSet;
        private Rectangle _collisionBox;

        /// <summary>
        /// The position on the screen where the <see cref="Sprite"/> should be drawn.
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// The angle that the sprite is drawn, in degrees. Incrementing this will rotate the
        /// sprite clockwise.
        /// </summary>
        public double Rotation { get; set; }

        /// <summary>
        /// The point around which the sprite will be rotated. By default, this will be set to
        /// the center of the sprite.
        /// </summary>
        public Point RotationCenter { get; set; }

        /// <summary>
        /// Indicates which axes this sprice should be flipped across, if any.
        /// </summary>
        public FlipDirection Flip { get; set; }

        /// <summary>
        /// The scale of the <see cref="Sprite"/>. 1.0f is 100%.
        /// </summary>
        public Vector2<float> Scale {
            get => _scale;
            set {
                _scale = value;
                _effectiveSize.X = (int)(Width * _scale.X);
                _effectiveSize.Y = (int)(Height * _scale.Y);

                if(!_collisionBoxSet)
                    _collisionBox = new Rectangle(new Point(0, 0), _effectiveSize);
            }
        }

        /// <inheritdoc/>
        public override ScalingQuality ScalingQuality {
            get => ScalingQualityValue;
            set {
                ScalingQualityValue = value;

                if(HasTexture) {
                    CreateTexture();
                    UpdateTexture();
                }
            }
        }

        /// <summary>
        /// Gets the size of the sprite as it would be drawn to the screen, in pixels, taking
        /// both scaling and clipping into account.
        /// </summary>
        /// <remarks>
        /// This value is calculated every time it's called. If you plan to the results of this
        /// in a loop, be sure to save the results to a variable and perform operations on that
        /// instead of calling this multiple times.
        /// </remarks>
        public Point DrawSize => new Point(
            (int)(Clipping.Size.X * Scale.X),
            (int)(Clipping.Size.Y * Scale.Y)
        );

        /// <summary>
        /// Gets the effective size of the sprite. This is the size of the sprite, in pixels,
        /// after taking scaling into account.
        /// </summary>
        public Point EffectiveSize => _effectiveSize;

        /// <summary>
        /// Defines the coordinate system that this sprite should use. This defaults to
        /// <see cref="CoordinateSystem.WorldSpace"/>.
        /// </summary>
        public CoordinateSystem CoordinateSystem { get; set; } = CoordinateSystem.WorldSpace;

        /// <summary>
        /// Gets or sets the size and position of the collision box for this <see cref="Sprite"/>.
        /// </summary>
        public Rectangle CollisionBox {
            get => _collisionBox;
            set {
                _collisionBox = value;
                _collisionBoxSet = true;
            }
        }

        /// <summary>
        /// Gets or sets whether or not collision calculations are performed on this <see cref="Sprite"/>.
        /// </summary>
        public bool HasCollision { get; set; }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height) : this(width, height, Point.Zero, Vector2<float>.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Point"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Point position) : this(width, height, position, Vector2<float>.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, int zOrder) : this(width, height, Point.Zero, Vector2<float>.One, zOrder) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="float"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2{T}"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        public Sprite(int width, int height, Point position, Vector2<float> scale) : this(width, height, position, scale, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Point"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, int zOrder) : this(width, height, position, Vector2<float>.One, 0) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Point"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2{T}"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, Vector2<float> scale, int zOrder)
            : this(width, height, position, new Rectangle(0, 0, width, height), scale, zOrder) { }

        /// <summary>
        /// Initializes a new <see cref="Sprite"/>.
        /// </summary>
        /// <param name="width">The width of the new <see cref="Sprite"/>.</param>
        /// <param name="height">The height of the new <see cref="Sprite"/>.</param>
        /// <param name="position">A <see cref="Point"/> representing the initial position of the new <see cref="Sprite"/>.</param>
        /// <param name="scale">A <see cref="Vector2{T}"/> representing the initial scaling of the new <see cref="Sprite"/>.</param>
        /// <param name="clipping">A rectangle specifying which part of this <see cref="Sprite"/> should be drawn.</param>
        /// <param name="zOrder">A value indicating the order in which this <see cref="Sprite"/> is drawn. Higher numbered
        /// sprites are drawn on top of other sprites and, thus, will appear above them.</param>
        public Sprite(int width, int height, Point position, Rectangle clipping, Vector2<float> scale, int zOrder) : base(width, height, clipping) {
            Position = position;
            Scale    = scale;
            ZOrder   = zOrder;
            Shown    = false;

            RotationCenter = new Point(clipping.Size.X / 2, clipping.Size.Y / 2);
        }

        /// <inheritdoc/>
        public override Point Center => new Point(
            (int)(Clipping.Size.X * _scale.X / 2),
            (int)(Clipping.Size.Y * _scale.Y / 2)
        );

        /// <summary>
        /// Determines whether or not a given point collides with this <see cref="Sprite"/>. By default
        /// this will test the point against the sprite's <see cref="CollisionBox"/>, but this method
        /// can be overridden.
        /// </summary>
        /// <param name="point">The <see cref="Point"/> to check.</param>
        /// <returns><c>true</c> if this object's collision is enabled and this sprite's collision
        /// routine determines that the given pixel collides with the sprite, otherwise <c>false</c>.</returns>
        public virtual bool CheckCollision(Point point) {
            if(!HasCollision) return false;

            return point.X >= CollisionBox.Position.X + Position.X
                && point.X <= CollisionBox.Position.X + CollisionBox.Size.X + Position.X
                && point.Y >= CollisionBox.Position.Y + Position.Y
                && point.Y <= CollisionBox.Position.Y + CollisionBox.Size.Y + Position.Y;
        }

        /// <summary>
        /// Determines whether or not a given point collides with this <see cref="Sprite"/>. By default
        /// this will test the point against the sprite's <see cref="CollisionBox"/>. If you wish to
        /// override this sprite's collision routine, override the <see cref="CheckCollision(DotSDL.Graphics.Point)"/>
        /// method instead.
        /// </summary>
        /// <param name="x">The X coordinate of the point to check.</param>
        /// <param name="y">The Y coordinate of the point to check.</param>
        /// <returns><c>true</c> if this object's collision is enabled and this sprite's collision
        /// routine determines that the given pixel collides with the sprite, otherwise <c>false</c>.</returns>
        public bool CheckCollision(int x, int y) => CheckCollision(new Point(x, y));

        /// <inheritdoc/>
        internal override void CreateTexture() {
            CreateTexture(Render.TextureAccess.Static);
        }

        /// <summary>
        /// Updates the texture associated with this <see cref="Sprite"/>. This function must be called when the
        /// <see cref="Canvas.Pixels"/> array is changed after adding this sprite to the sprite list associated
        /// with the application's <see cref="SdlWindow"/>.
        /// </summary>
        /// <returns><c>true</c> if the texture was successfully updated, otherwise <c>false</c>. This will return
        /// <c>false</c> if this <see cref="Sprite"/> hasn't been added to the sprite list.</returns>
        public new bool UpdateTexture() {
            return base.UpdateTexture();
        }
    }
}
