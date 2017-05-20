namespace DotSDL {
    /// <summary>
    /// Specifies an object that contains a resource.
    /// </summary>
    internal interface IResourceObject {
        /// <summary>
        /// <c>true</c> if the underlying <see cref="IResourceObject"/> has been destoryed, otherwise <c>false</c>.
        /// </summary>
        bool IsDestroyed { get; set; }

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
