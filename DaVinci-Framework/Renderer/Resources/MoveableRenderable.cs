using System;

namespace DaVinci_Framework.Renderer.Resources
{
    public class MoveableRenderable : Renderable
    {
        private bool _canMove; // If the renderable is allowed to move yet
        private System.Timers.Timer _timer; // The delta-time timer
        private double _multiplier; // If you want to change the multiplier of the delta-time

        public MoveableRenderable(double[] initialPosition) : base(initialPosition)
        {
            // Set the initial state of the private variables
            _canMove = true;
            _multiplier = 1.0f;

            CreateTimer();
        }

        /// <summary>
        /// Moves the item to a new position
        /// </summary>
        /// <param name="newPosition">The new position the item is to be moved to</param>
        public virtual void Move(double[] newPosition)
        {
            if (_canMove) // If it is allowed to move
            {
                _canMove = false; // Flag that you can't move again until delta time has reset it
                _changed = true; // Flag up that the item has changed
                _prevPosition = _position; // Store the old position in the previous position variable
                _position = newPosition; // Overwrites the position with the new position
            }
        }

        /// <summary>
        /// Creates the timer that will control the delta-time
        /// </summary>
        public void CreateTimer()
        {
            _timer = new System.Timers.Timer(100.0 * (1.0 / _multiplier)); // Create the timer
            _timer.Enabled = true; // Make it publish events
            _timer.AutoReset = true; // Get it to reset
            _timer.Elapsed += OnElapsed; // Subscribe this item to the event
        }

        /// <summary>
        /// Changes the multipler of the delta-time, this will allow for the item to tick faster or slower
        /// </summary>
        /// <param name="newMult"></param>
        public void ChangeMultiplier(float newMult)
        {
            _multiplier = newMult; // Reset the multiplier
            _timer.Dispose(); // Dispose of the old timer

            CreateTimer(); // Create a new timer.

        }

        /// <summary>
        /// The elapsed event for the delta-time timer
        /// </summary>
        /// <param name="source">The timer itself</param>
        /// <param name="args">Any arguments passed</param>
        public void OnElapsed(object source, EventArgs args)
        {
            _canMove = true; // Allow it to move again
        }
    }
}
