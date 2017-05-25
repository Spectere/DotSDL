namespace DotSDL {
    /// <summary>
    /// Specifies an object that contains a resource.
    /// </summary>
    public interface IResourceObject {
        /// <summary>
        /// <c>true</c> if the underlying <see cref="IResourceObject"/> has been destroyed, otherwise <c>false</c>.
        /// </summary>
        bool IsDestroyed { get; set; }

        /// <summary>
        /// The type of resource contained in this instance.
        /// </summary>
        ResourceType ResourceType { get; }

        /// <summary>
        /// Destroys this <see cref="IResourceObject"/>.
        /// </summary>
        void DestroyObject();

        /// <summary>
        /// Retrieves the SDL resource ID for the object instance..
        /// </summary>
        /// <returns>The reousrce ID for the object instance.</returns>
        uint GetResourceId();
    }
}
