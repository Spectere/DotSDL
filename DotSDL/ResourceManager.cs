using System;

namespace DotSDL {
    /// <summary>
    /// This singleton keeps track of all the numbered SDL resources in the
    /// system (window IDs, joystick IDs, etc). This is used to help dispatch
    /// events.
    /// </summary>
    internal sealed class ResourceManager {
        private static readonly Lazy<ResourceManager> Singleton = new Lazy<ResourceManager>(() => new ResourceManager());
        internal static ResourceManager Instance => Singleton.Value;


    }
}
