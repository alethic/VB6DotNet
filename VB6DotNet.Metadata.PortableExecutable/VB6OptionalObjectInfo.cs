using System;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    public readonly struct VB6OptionalObjectInfo
    {

        internal const int Size = 64;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6OptionalObjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

    }

}

