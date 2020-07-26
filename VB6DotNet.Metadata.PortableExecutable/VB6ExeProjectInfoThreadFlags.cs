﻿using System;

namespace VB6DotNet.Metadata.PortableExecutable
{

    [Flags]
    public enum VB6ExeProjectInfoThreadFlags : uint
    {

        ApartmentModel = 0x1,
        RequireLicense = 0x2,
        Unattended = 0x4,
        SingleThreaded = 0x8,
        Retained = 0x10,

    }

}