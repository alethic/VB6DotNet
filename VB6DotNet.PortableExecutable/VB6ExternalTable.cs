using System;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    public readonly struct VB6ExternalTable
    {

        internal const int Size = 0;

        readonly PEReader pe;
        readonly int offset;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ExternalTable(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

    }

}
