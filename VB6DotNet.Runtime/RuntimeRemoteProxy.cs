using System;

using VB6DotNet.Runtime.Threading;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Represents a remote proxy to a COM object.
    /// </summary>
    public class RuntimeRemoteProxy : RuntimeObject
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="apartment"></param>
        public RuntimeRemoteProxy(RuntimeLocalProxy remote, Apartment apartment) :
            base(apartment.GetProxyType(remote.Object.Type), apartment)
        {
            Remote = remote ?? throw new ArgumentNullException(nameof(remote));
        }

        /// <summary>
        /// Interface to the remote object.
        /// </summary>
        public RuntimeLocalProxy Remote { get; }

        /// <summary>
        /// Generates an interface that allows remote calls to the proxied object.
        /// </summary>
        /// <param name="iid"></param>
        /// <returns></returns>
        public override RuntimeInterface QueryInterface(Guid iid)
        {
            throw new NotImplementedException();
        }

    }

}
