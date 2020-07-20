using System;

namespace VB6DotNet.PortableExecutable
{

    [Flags]
    public enum VB6ExeProjectInfoControlFlags2 : uint
    {

        DataQueryObject = 0x20,
        OleObject = 0x40,
        UserControlObject = 0x100,
        PropertyPageObject = 0x200,
        DocumentObject = 0x400,

    }

}
