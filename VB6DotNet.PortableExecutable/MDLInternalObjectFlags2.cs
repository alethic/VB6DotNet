using System;

namespace VB6DotNet.PortableExecutable
{

    [Flags]
    public enum MDLInternalObjectFlags2 : uint
    {

        DataQuery = 0x00000020,
        OLE = 0x00000040,
        UserControl = 0x00000100,
        PropertyPage = 0x00000200,
        Document = 0x00000400,

    }

}
