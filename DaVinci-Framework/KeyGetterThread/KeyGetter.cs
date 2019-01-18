using System.Threading;

namespace DaVinci_Framework.KeyGetterThread
{
    /// <summary>
    /// Starts a thread to run the keyGetter
    /// </summary>
    public class KeyGetter
    {
        /// <summary>
        /// Will start the listening thread
        /// </summary>
        /// <param name="listener">Listener object</param>
        public static void Run(Listener listener)
        {
            var threadRef = new ThreadStart(listener.Listen); // Assign what function the thread will run

            var listenerThread = new Thread(threadRef); // Create the thread object

            listenerThread.Start(); // Start the listener thread

        }
    }
}
