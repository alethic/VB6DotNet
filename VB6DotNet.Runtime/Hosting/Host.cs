using System;
using System.Collections.Generic;

using VB6DotNet.Runtime.Threading;

namespace VB6DotNet.Runtime.Hosting
{

    /// <summary>
    /// The root container for VB6 runtime functionality.
    /// </summary>
    public class Host : IDisposable
    {

        readonly MultiThreadedApartment mta;
        readonly List<SingleThreadedApartment> sta;

        bool disposed;

        /// <summary>
        /// Initializes a new host.
        /// </summary>
        public Host()
        {
            mta = new MultiThreadedApartment(this);
            sta = new List<SingleThreadedApartment>();
        }

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    mta.Dispose();

                    foreach (var i in new List<SingleThreadedApartment>(sta))
                        i.Dispose();

                    sta.Clear();
                }

                disposed = true;
            }
        }

        /// <summary>
        /// Disposes of the instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes the instance.
        /// </summary>
        ~Host()
        {
            Dispose(false);
        }

    }

}
