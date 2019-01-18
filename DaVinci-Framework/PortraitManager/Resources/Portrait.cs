using DaVinci_Framework.Renderer.Resources;

namespace DaVinci_Framework.PortraitManager.Resources
{
    /// <summary>
    /// Base class for all screens
    /// </summary>
    public class Portrait
    {
        protected Register _register; // Register to hold the portrait's renderable items
        protected Renderer.Renderer _renderer; // Rendering object
        protected Display.Display _display; // Display object
        protected Manager _manager; // The portrait manager this portrait is associated with
        protected bool _active; // If the portrait is active or not

        /// <summary>
        /// Selects this portrait as the current portrait
        /// </summary>
        public virtual void SelectThisPortrait()
        {
            _renderer.SetRegister(_register); // Tells the renderer to render this portrait's register
            _active = true;
        }

        /// <summary>
        /// When the portrait is deselected, this method will be called.
        /// </summary>
        public virtual void DeSelectThisPortrait()
        {
            _active = false;
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        public void Draw()
        {
            _display.PaintScreen(_renderer.Render()); // Paint the screen
        }

        /// <summary>
        /// Advances one logical tick, can be overridden.
        /// </summary>
        public virtual void Tick()
        {
            Draw();
        }

        /// <summary>
        /// Set the current manager
        /// </summary>
        /// <param name="manager">The manager responsible for this portrait</param>
        public void SetManager(Manager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Set the rendering options
        /// </summary>
        /// <param name="renderer">The render object</param>
        /// <param name="display">The display object</param>
        public void SetRenderOptions(Renderer.Renderer renderer, Display.Display display)
        {
            _renderer = renderer;
            _display = display;
        }
    }
}
