using System;
using System.Collections.Generic;

using VB6DotNet.Metadata;
using VB6DotNet.Runtime.Hosting;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes a VB6 class module type.
    /// </summary>
    class RuntimeClassObjectType : RuntimeObjectType
    {

        /// <summary>
        /// Builds the interfaces into a VB6 class module.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="classInfo"></param>
        /// <returns></returns>
        static IReadOnlyList<RuntimeInterfaceType> BuildInterfaceTypes(Host host, ClassInfo classInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="classInfo"></param>
        public RuntimeClassObjectType(Host host, ClassInfo classInfo) :
            base(classInfo.Id, classInfo.Name, BuildInterfaceTypes(host, classInfo))
        {

        }

        /// <summary>
        /// Gets the runtime host that owns this class type.
        /// </summary>
        public Host Host { get; }

        /// <summary>
        /// Gets the VB6 class module information.
        /// </summary>
        public ClassInfo ClassInfo { get; }

        /// <summary>
        /// Generates the specified interface implementation for this class type.
        /// </summary>
        /// <param name="iid"></param>
        /// <returns></returns>
        public RuntimeInterface QueryInterface(Guid iid)
        {
            throw new NotImplementedException();
        }

    }

}
