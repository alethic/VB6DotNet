
using VB6DotNet.Metadata.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Represents a VB6 module object.
    /// </summary>
    public class ModuleInfo : ObjectInfo
    {

        internal static ModuleInfo Load(VB6Object src)
        {
            var m = new ModuleInfo();
            Load(src, m);
            return m;
        }

    }

}
