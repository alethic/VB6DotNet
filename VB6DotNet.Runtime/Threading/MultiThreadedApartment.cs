using System;
using System.Threading;

using VB6DotNet.Runtime.Hosting;

namespace VB6DotNet.Runtime.Threading
{

    /// <summary>
    /// Implements a COM multi-threaded apartment. Calls are dispatched directly and immediately.
    /// </summary>
    public class MultiThreadedApartment : Apartment
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="host"></param>
        public MultiThreadedApartment(Host host) :
            base(host)
        {

        }

        public override void Dispatch(Action<CancellationToken> action)
        {
            action(CancellationToken.None);
        }

    }

}
