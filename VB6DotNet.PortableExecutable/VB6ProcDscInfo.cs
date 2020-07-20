using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    public readonly struct VB6ProcDscInfo
    {

        internal const int Size = 572;

        readonly PEReader pe;
        readonly int offset;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6ProcDscInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        /// <summary>
        /// Gets the procedure code.
        /// </summary>
        public VB6ProcCode ProcCode => new VB6ProcCode(pe, offset - ProcSize, ProcSize);

        /// <summary>
        /// Obtains the memory range of the <see cref="VB6ProcDscInfo"/> structure.
        /// </summary>
        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets the pointer to the procedure table.
        /// </summary>
        int ProcTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Gets the procedure table.
        /// </summary>
        public VB6ProcTable ProcTable => new VB6ProcTable(pe, ProcTablePtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Unknown.
        /// </summary>
        short Field4 => BinaryPrimitives.ReadInt16LittleEndian(Span[0x4..0x6]);

        /// <summary>
        /// Gets the size of the frames.
        /// </summary>
        public short FrameSize => BinaryPrimitives.ReadInt16LittleEndian(Span[0x6..0x8]);

        /// <summary>
        /// Gets the length of the procedure.
        /// </summary>
        public short ProcSize => BinaryPrimitives.ReadInt16LittleEndian(Span[0x8..0xa]);

    }

}

