using System;
using System.Collections.Generic;
using System.Linq;

namespace DotSDL {
    /// <summary>
    /// This singleton keeps track of all the numbered SDL resources in the
    /// system (window IDs, joystick IDs, etc). This is used to help dispatch
    /// events.
    /// </summary>
    internal sealed class ResourceManager {
        private static readonly Lazy<ResourceManager> Singleton = new Lazy<ResourceManager>(() => new ResourceManager());
        internal static ResourceManager Instance => Singleton.Value;

        private readonly List<IResourceObject> _resources = new List<IResourceObject>();

        ~ResourceManager() {
            UnregisterAllResources();
        }

        /// <summary>
        /// Retrieves a resources matching a given ID.
        /// </summary>
        /// <param name="id">The ID number of the <see cref="IResourceObject"/> to retrieve.</param>
        /// <returns>The <see cref="IResourceObject"/> with a matching ID, or null if it doesn't exist.</returns>
        internal IResourceObject GetResourceById(uint id) {
            return _resources.FirstOrDefault(e => e.GetResourceId() == id);
        }

        /// <summary>
        /// Retrieves all resources matching a given type.
        /// </summary>
        /// <param name="type">The <see cref="ResourceType"/> that should be returned.</param>
        /// <returns>A collection of <see cref="Resource"/>s matching the type given in <paramref name="type"/>.</returns>
        internal IEnumerable<IResourceObject> GetResourceByType(string type) {
            //return _resources.Where(e => e is typeof(type));
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers a resource with the <see cref="ResourceManager"/>.
        /// </summary>
        /// <param name="resource">The resource to register.</param>
        internal void RegisterResource(IResourceObject resource) {
            _resources.Add(resource);
        }

        /// <summary>
        /// Destroys and unregisters all <see cref="IResourceObject"/>s from the <see cref="ResourceManager"/>.
        /// </summary>
        internal void UnregisterAllResources() {
            foreach(var resource in _resources) {
                if(!resource.IsDestroyed)
                    resource.DestroyObject();
            }

            _resources.Clear();
        }

        /// <summary>
        /// Unregisters a resource from the <see cref="ResourceManager"/>.
        /// </summary>
        /// <param name="resource">The resource to unregister.</param>
        /// <returns><c>true</c> if the resource was successfully removed, otherwise <c>false</c>.</returns>
        internal bool UnregisterResource(IResourceObject resource) {
            var resourceObject = _resources.FirstOrDefault(e => e == resource);
            if(resourceObject is null) return false;

            if(!resourceObject.IsDestroyed)
                resourceObject.DestroyObject();

            _resources.Remove(resourceObject);
            return true;
        }
    }
}
