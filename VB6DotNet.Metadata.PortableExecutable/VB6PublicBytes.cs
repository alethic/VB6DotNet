using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    public readonly struct VB6PublicBytes
    {

        internal const int Size = 4;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6PublicBytes(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        public short StringBytes => BinaryPrimitives.ReadInt16LittleEndian(Span.Slice(0, 2));

        public short VarBytes => BinaryPrimitives.ReadInt16LittleEndian(Span.Slice(2, 2));

    }

}

