using System;
using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes the type of an object at runtime.
    /// </summary>
    public class RuntimeObjectType : RuntimeType
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        public RuntimeObjectType(Guid clsId, string name, IReadOnlyList<RuntimeInterfaceType> interfaces) :
            base(name)
        {
            ClsId = clsId;
            Interfaces = interfaces ?? throw new ArgumentNullException(nameof(interfaces));
        }

        /// <summary>
        /// Gets the CLSID of the type.
        /// </summary>
        public Guid ClsId { get; }

        /// <summary>
        /// Gets the interfaces available on the object type.
        /// </summary>
        public IReadOnlyList<RuntimeInterfaceType> Interfaces { get; }

    }

}
