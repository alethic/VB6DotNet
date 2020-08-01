using System;
using System.Collections.Generic;

using VB6DotNet.Metadata.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes the basic object type.
    /// </summary>
    public abstract class ObjectInfo
    {

        /// <summary>
        /// Loads the 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        internal static void Load(VB6Object src, ObjectInfo dst)
        {
            dst.Name = src.ObjectName;

            for (var i = 0; i < src.ObjectInfo.ProcedureCount; i++)
            {
                var p = src.ObjectInfo.Procedures[i];
                var n = src.ProcedureNames.Count > i ? src.ProcedureNames[i] : null;
                
            }
        }

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of members within this module.
        /// </summary>
        public ICollection<MemberInfo> Members { get; set; } = new List<MemberInfo>();

    }

}
