using System;

namespace DotSDL {
    /// <summary>
    /// Describes an SDL resource. These objects are tracked using the
    /// <see cref="ResourceManager"/>.
    /// </summary>
    internal class Resource {
        /// <summary>
        /// The type of resource represeented by this instance.
        /// </summary>
        internal ResourceType Type { get; set; }

        /// <summary>
        /// The numeric ID of the resource.
        /// </summary>
        internal uint ResourceId { get; set; }

        /// <summary>
        /// The pointer to the resource's SDL object.
        /// </summary>
        internal IntPtr ResourcePtr { get; set; }
    }
}
