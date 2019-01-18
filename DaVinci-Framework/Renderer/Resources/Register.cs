using System.Collections.Generic;

namespace DaVinci_Framework.Renderer.Resources
{
    /// <summary>
    /// A register to hold all the of renderable items.
    /// </summary>
    public class Register
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Renderable> _renderables; // A list of all the registered items.
        private List<Renderable> _changedItems; // A list of all the items that have changed since last rendered.
        private List<Renderable> _deletedItems; // A list of all the items that have been deleted since last rendered.
        private int[] _screenSize; // The size of the screen this register uses.

        /// <summary>
        /// Instantiate the register class and create the renderables variable
        /// </summary>
        public Register(int screenWidth = -1, int screenHeight = -1)
        {
            _renderables = new List<Renderable>(); // Create the list of items

            _screenSize = new[] { 120, 30 };
            if (screenWidth > 0 && screenHeight > 0)
                _screenSize = new[] { screenWidth, screenHeight };
        }

        /// <summary>
        /// Register an item to the instance
        /// </summary>
        /// <param name="item">The item to be registered</param>
        public void RegisterItem(Renderable item)
        {
            _renderables.Add(item); // Add an item to the register.
        }

        /// <summary>
        /// Un-register an item from the instance
        /// </summary>
        /// <param name="item">The item to be un-registered</param>
        public void UnregisterItem(Renderable item)
        {
            item.Delete(); // Call the delete method of the item
        }

        /// <summary>
        /// Get the items in the list that have been changed
        /// </summary>
        /// <returns>A list of all the changed items.</returns>
        public List<Renderable> GetChangedItems()
        {
            _changedItems = new List<Renderable>(); // Create a blank of changed items
            _deletedItems = new List<Renderable>(); // Create a blank of deleted items

            if (_renderables.Count > 0) // If the list isn't empty, go through the list
            {
                foreach (var item in _renderables) // Go through each of the items
                {
                    if (item.HasChanged()) // If the item has changed
                        _changedItems.Add(item); item.ResetChanged(false); // Add the item to the changed items list and reset the changed value

                    if (item.HasBeenDeleted()) // If the item has ben deleted
                        _deletedItems.Add(item); // Add to the deleted items list
                }
            }

            return _changedItems; // Return the changed items.
        }

        /// <summary>
        /// Remove the items from the register and return the list of them
        /// </summary>
        /// <returns></returns>
        public List<Renderable> GetDeletedItems()
        {

            foreach (var item in _deletedItems)
            {
                _renderables.Remove(item); // Remove the item from the register.
            }

            return _deletedItems;

        }

        /// <summary>
        /// Go through each item and reset the changed value
        /// </summary>
        public void Refresh()
        {
            foreach (var item in _renderables)
            {
                item.ResetChanged(true); // Set the changed value to true
            }
        }

        /// <summary>
        /// Returns the screen size
        /// </summary>
        /// <returns>The screen size this register uses</returns>
        public int[] getScreenSize()
        {
            return _screenSize;
        }
    }
}
