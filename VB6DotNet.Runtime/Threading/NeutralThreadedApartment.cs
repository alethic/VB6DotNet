using System;
using System.Threading;

using VB6DotNet.Runtime.Hosting;

namespace VB6DotNet.Runtime.Threading
{

    /// <summary>
    /// Implements a COM neutral-threaded apartment.
    /// </summary>
    public class NeutralThreadedApartment : Apartment
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="host"></param>
        public NeutralThreadedApartment(Host host) : 
            base(host)
        {

        }

        public override void Dispatch(Action<CancellationToken> action)
        {
            throw new NotImplementedException();
        }

    }

}
