using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    public readonly struct VB6ProcTable
    {

        internal const int Size = 56;

        readonly PEReader pe;
        readonly int offset;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6ProcTable(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Unknown.
        /// </summary>
        ReadOnlySpan<byte> Unknown => Span[0..52];

        /// <summary>
        /// Gets the data constant.
        /// </summary>
        int DataConstPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[52..56]);

    }

}

