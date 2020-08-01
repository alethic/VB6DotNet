using System;
using System.Threading;

namespace VB6DotNet.Runtime.Threading
{

    /// <summary>
    /// Information regarding the COM-emulated thread attached to a .NET thread.
    /// </summary>
    public class Thread
    {


        /// <summary>
        /// Gets the COM-emulated thread information for the current .NET thread.
        /// </summary>
        public static Thread CurrentThread => _currentThread.Value;
        static ThreadLocal<Thread> _currentThread = new ThreadLocal<Thread>(() => new Thread(System.Threading.Thread.CurrentThread));

        readonly System.Threading.Thread native;

        // describes thread initialization
        int coCall;
        CoInit coInit;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="native"></param>
        public Thread(System.Threading.Thread native)
        {
            this.native = native ?? throw new ArgumentNullException(nameof(native));
        }

        /// <summary>
        /// Initializes the current thread.
        /// </summary>
        /// <param name="model"></param>
        public void CoInitialize(CoInit coInit)
        {
            if (coCall > 0 && coInit != this.coInit)
                throw new InvalidOperationException();

            // increment reentry counter
            coCall++;

            // set thread mode
            this.coInit = coInit;
        }

        /// <summary>
        /// Uninitializes the current thread.
        /// </summary>
        public void CoUninitialize()
        {
            if (coCall == 0)
                throw new InvalidOperationException();

            // decrement reentry counter
            coCall--;

            // unset thread mode
            if (coCall == 0)
                coInit = CoInit.None;
        }

    }

}
