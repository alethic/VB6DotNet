
using VB6DotNet.Metadata.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Represents a VB6 module object.
    /// </summary>
    public class Module : Object
    {

        internal static Module Load(VB6Object src)
        {
            var m = new Module();
            Load(src, m);
            return m;
        }

    }

}
