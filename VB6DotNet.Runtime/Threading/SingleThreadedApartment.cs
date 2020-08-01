using System;
using System.Collections.Concurrent;
using System.Threading;

using VB6DotNet.Runtime.Hosting;

namespace VB6DotNet.Runtime.Threading
{

    /// <summary>
    /// Implements a COM single-threaded apartment, with a work queue and dispatcher thread.
    /// </summary>
    public class SingleThreadedApartment : Apartment
    {

        readonly System.Threading.Thread thread;
        readonly BlockingCollection<Action<CancellationToken>> queue;

        /// <summary>
        /// Initializes a new STA.
        /// </summary>
        /// <param name="host"></param>
        public SingleThreadedApartment(Host host) :
            base(host)
        {
            // holds messages to be executed
            queue = new BlockingCollection<Action<CancellationToken>>();

            // begin message queue
            thread = new System.Threading.Thread(DoEventsMain);
            thread.Start();
        }

        /// <summary>
        /// Dispatches an action to run on the apartment thread.
        /// </summary>
        /// <param name="action"></param>
        public override void Dispatch(Action<CancellationToken> action)
        {
            CancellationToken.ThrowIfCancellationRequested();
            queue.Add(action, CancellationToken);
        }

        /// <summary>
        /// Main loop of STA.
        /// </summary>
        /// <param name="state"></param>
        void DoEventsMain(object state)
        {
            Thread.CurrentThread.CoInitialize(CoInit.ApartmentThreaded);
            DoEvents();
            Thread.CurrentThread.CoUninitialize();
        }

        /// <summary>
        /// Main loop of STA.
        /// </summary>
        public void DoEvents()
        {
            // only allow reentry by the original thread
            if (System.Threading.Thread.CurrentThread != thread)
                throw new InvalidOperationException();

            while (CancellationToken.IsCancellationRequested == false)
                if (queue.Take(CancellationToken) is Action<CancellationToken> action)
                    DoEvent(action, CancellationToken);
        }

        /// <summary>
        /// Executes a particular action.
        /// </summary>
        /// <param name="cancellationToken"></param>
        void DoEvent(Action<CancellationToken> action, CancellationToken cancellationToken)
        {
            try
            {
                action(cancellationToken);
            }
            catch (Exception e)
            {
                // continue dispatching unhandled exception
                // this could cause an exception loop if the handler is unable to execute
                DoEvent(c => UnhandledException?.Invoke(this, new UnhandledExceptionEventArgs(e, false)), cancellationToken);
            }
        }

        /// <summary>
        /// Raised when an unhandled exception occurs on one of the action delegates.
        /// </summary>
        public event UnhandledExceptionEventHandler UnhandledException;

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            // dispose of the base instance, which causes cancellation
            base.Dispose(disposing);

            // wait for our queue thread to exit
            if (disposing)
                thread.Join();
        }

    }

}
