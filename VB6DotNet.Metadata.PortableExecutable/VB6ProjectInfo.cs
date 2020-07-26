using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    public readonly struct VB6ProjectInfo
    {

        internal const int Size = 572;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ProjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Version.
        /// </summary>
        public int Version => BinaryPrimitives.ReadInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Pointer to the object table.
        /// </summary>
        int ObjectTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Gets the object table.
        /// </summary>
        public VB6ObjectTable ObjectTable => new VB6ObjectTable(pe, ObjectTablePtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Unused value after compilation.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Points to the start of the code. Unused.
        /// </summary>
        int CodeStartPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Points to the end of the code. Unused.
        /// </summary>
        int CodeEndPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// Size of VB object structures. Unused.
        /// </summary>
        public int DataSize => BinaryPrimitives.ReadInt32LittleEndian(Span[0x14..0x18]);

        /// <summary>
        /// Pointer to pointer to Thread object.
        /// </summary>
        int ThreadSpacePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x18..0x1c]);

        /// <summary>
        /// Pointer to the VBA exception handler.
        /// </summary>
        int VbaSehPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x1c..0x20]);

        /// <summary>
        /// Pointer to the .data section.
        /// </summary>
        int NativeCodePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x20..0x24]);

        short ProjectLocation => BinaryPrimitives.ReadInt16LittleEndian(Span.Slice(0x24, 2));

        short Flag2 => BinaryPrimitives.ReadInt16LittleEndian(Span.Slice(0x26, 2));

        short Flag3 => BinaryPrimitives.ReadInt16LittleEndian(Span.Slice(0x28, 2));

        /// <summary>
        /// Contains Path and ID string.
        /// </summary>
        public ReadOnlySpan<byte> OriginalPathName => Span[0x2a..0x234];

        /// <summary>
        /// Pointer to import table.
        /// </summary>
        int ExternalTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x234..0x238]);

        /// <summary>
        /// Gets the import table.
        /// </summary>
        public VB6ExternalTable ExternalTable => new VB6ExternalTable(pe, ExternalTablePtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Number of imports.
        /// </summary>
        public int ExternalCount => BinaryPrimitives.ReadInt32LittleEndian(Span[0x238..0x23c]);

    }

}

