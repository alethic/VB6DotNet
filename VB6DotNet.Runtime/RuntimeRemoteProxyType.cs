using System;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Represents a remote proxy to a COM object.
    /// </summary>
    public class RuntimeRemoteProxyType : RuntimeObjectType
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="remoteType"></param>
        public RuntimeRemoteProxyType(RuntimeObjectType remoteType) :
            base(Guid.NewGuid(), $"RuntimeRemoteProxyType<{remoteType.Name}>", remoteType.Interfaces)
        {

        }

    }

}
