using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.Instructions.Resources
{
    /// <summary>
    /// Struct to keep track of pages for instructions.
    /// </summary>
    public struct Page
    {
        private Register _register; // The register it writes to.
        private DrawHandler _drawFunction; // The function it calls to draw.

        public delegate void DrawHandler(Register register); // The delegate signature for the draw function

        public Page(Register register, DrawHandler drawFunction)
        {
            _register = register;
            _drawFunction = drawFunction;
        }

        /// <summary>
        /// Draw the page.
        /// </summary>
        public void Draw()
        {
            _drawFunction(_register);
        }
    }
}
