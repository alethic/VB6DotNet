using System;
using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes a COM interface to an object instance at runtime.
    /// </summary>
    public class RuntimeInterfaceType : RuntimeType
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="methods"></param>
        /// <param name="parent"></param>
        public RuntimeInterfaceType(Guid id, string name, IReadOnlyList<RuntimeMethodType> methods, RuntimeInterfaceType parent) :
            base(name)
        {
            Id = id;
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        /// <summary>
        /// Gets the IID of the interface.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the parent interface type.
        /// </summary>
        public RuntimeInterfaceType Parent { get; }

        /// <summary>
        /// Gets the methods available on the interface.
        /// </summary>
        public IReadOnlyList<RuntimeMethodType> Methods { get; }

    }

}