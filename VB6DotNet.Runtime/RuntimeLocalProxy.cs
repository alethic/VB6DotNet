using System;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Provides an interface through which remote <see cref="RuntimeRemoteProxy"/> instances can communicate with apartment local objects.
    /// </summary>
    public class RuntimeLocalProxy
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="object"></param>
        public RuntimeLocalProxy(RuntimeObject @object)
        {
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        /// <summary>
        /// Reference to the object being exposed.
        /// </summary>
        public RuntimeObject Object { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        public void Invoke(RuntimeInvocation call)
        {
            Object.Apartment.Dispatch((cancellationToken) =>
            {
                Object.
            });
        }

    }

}
