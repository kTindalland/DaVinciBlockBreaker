using System.Collections.Generic;
using DaVinci_Framework.PortraitManager.Resources;

namespace DaVinci_Framework.PortraitManager
{
    /// <summary>
    /// This class will manage which portraits will be 2
    /// </summary>
    public class Manager
    {
        private readonly Renderer.Renderer _renderer;
        private readonly Display.Display _display;
        private int _currentPortrait;
        public Dictionary<int, Portrait> Portraits;

        /// <summary>
        /// Initialise the manaager with an initial portrait
        /// </summary>
        /// <param name="renderer">The rendering object</param>
        /// <param name="display">The display object</param>
        /// <param name="startID">The id of the starting portrait</param>
        /// <param name="startPortrait">The starting portrait object</param>
        public Manager(Renderer.Renderer renderer, Display.Display display, int startID, Portrait startPortrait)
        {
            _renderer = renderer;
            _display = display;
            _currentPortrait = startID;

            Portraits = new Dictionary<int, Portrait>();

            AddPortrait(_currentPortrait, startPortrait); // Add the initial portrait to the list.
            startPortrait.SetManager(this);
            Portraits[_currentPortrait].SelectThisPortrait(); // Initialise the portrait

        }

        /// <summary>
        /// Switch the current portrait to a different one
        /// </summary>
        /// <param name="newPortrait">The id of the portrait you want to switch to</param>
        public void SwitchCurrentPortrait(int newPortrait)
        {
            Portraits[_currentPortrait].DeSelectThisPortrait(); // Deselect the old portrait
            Portraits[newPortrait].SelectThisPortrait(); // Select the new portrait
            _currentPortrait = newPortrait; // Reassign the current portrait variable
        }

        /// <summary>
        /// Add a portrait to the dictionary
        /// </summary>
        /// <param name="id">The id for which kind of portrait it is</param>
        /// <param name="portrait">The actual portrait object you'd like to assign to that ID</param>
        public void AddPortrait(int id, Portrait portrait)
        {
            Portraits.Add(id, portrait); // Add it to the dictionary
            portrait.SetManager(this); // Set the portraits manager to this instance
            portrait.SetRenderOptions(_renderer, _display); // Set the portrait rendering options
        }

        /// <summary>
        /// A standard logical tick
        /// </summary>
        public void Tick()
        {
            Portraits[_currentPortrait].Tick(); // Increase the portraits logic by one step
        }
    }
}
