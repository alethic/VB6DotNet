using System;
using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes a COM interface to an object instance at runtime.
    /// </summary>
    public class RuntimeInterface
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="interface"></param>
        /// <param name="object"></param>
        /// <param name="methods"></param>
        public RuntimeInterface(RuntimeInterfaceType @interface, RuntimeObject @object, IReadOnlyList<RuntimeMethod> methods)
        {
            Interface = @interface ?? throw new ArgumentNullException(nameof(@interface));
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }

        /// <summary>
        /// Gets the type of interface implemented.
        /// </summary>
        public RuntimeInterfaceType Interface { get; }

        /// <summary>
        /// Gets the object associated with the interface.
        /// </summary>
        public RuntimeObject Object { get; }

        /// <summary>
        /// Gets the method implementations provided by this interface.
        /// </summary>
        public IReadOnlyList<RuntimeMethod> Methods { get; }

    }

}