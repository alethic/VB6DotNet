using System;

using VB6DotNet.Metadata.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Represents a VB6 module object.
    /// </summary>
    public class ClassModule : Object
    {

        internal static Object Load(VB6Object o)
        {
            throw new NotImplementedException();
        }

        public Instancing Instancing { get; set; }

    }

}
