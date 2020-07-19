using System;

namespace VB6DotNet.Metadata
{

    [Flags]
    public enum VB6RegInfoObjectTypes : byte
    {

        Designer = 0x2,
        ClassModule = 0x10,
        UserControl = 0x20,
        UserDocument = 0x80,

    }

}
