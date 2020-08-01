using System;
using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Encapsulates a method call.
    /// </summary>
    public class RuntimeInvocation
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        public RuntimeInvocation(RuntimeInterfaceType target, RuntimeMethodType method, IReadOnlyList<object> args)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Method = method ?? throw new ArgumentNullException(nameof(method));
            Args = args ?? throw new ArgumentNullException(nameof(args));
        }

        /// <summary>
        /// Gets the interface to invoke.
        /// </summary>
        public RuntimeInterfaceType Target { get; }

        /// <summary>
        /// Gets the method to invoke.
        /// </summary>
        public RuntimeMethodType Method { get; }

        /// <summary>
        /// Gets the set of arguments passed with the call.
        /// </summary>
        public IReadOnlyList<object> Args { get; }

    }

}
