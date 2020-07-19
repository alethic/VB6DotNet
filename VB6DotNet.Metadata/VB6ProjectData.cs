using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    public readonly ref struct VB6ProjectData
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ProjectData(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 572);
        }

        /// <summary>
        /// Version.
        /// </summary>
        public int Version => BinaryPrimitives.ReadInt32LittleEndian(memory[0x0..0x4]);

        /// <summary>
        /// Gets the project data.
        /// </summary>
        public VB6ObjectTable ObjectTable => new VB6ObjectTable(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x4..0x8]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Unused value after compilation.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(memory[0x8..0xc]);

        /// <summary>
        /// Points to the start of the code. Unused.
        /// </summary>
        public int CodeStartPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0xc..0x10]);

        /// <summary>
        /// Points to the end of the code. Unused.
        /// </summary>
        public int CodeEndPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x10..0x14]);

        /// <summary>
        /// Size of VB object structures. Unused.
        /// </summary>
        public int DataSize => BinaryPrimitives.ReadInt32LittleEndian(memory[0x14..0x18]);

        /// <summary>
        /// Pointer to pointer to Thread object.
        /// </summary>
        public int ThreadSpacePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x18..0x1c]);

        /// <summary>
        /// Pointer to the VBA exception handler.
        /// </summary>
        public int VbaSehPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x1c..0x20]);

        /// <summary>
        /// Pointer to the .data section.
        /// </summary>
        public int NativeCodePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x20..0x24]);

        /// <summary>
        /// Contains Path and ID string.
        /// </summary>
        public ReadOnlySpan<byte> PathInformation => memory[0x24..0x234];

        /// <summary>
        /// External table.
        /// </summary>
        public int ExternalTablePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x234..0x238]);

        /// <summary>
        /// Objects in the external table.
        /// </summary>
        public int ExternalCount => BinaryPrimitives.ReadInt32LittleEndian(memory[0x238..0x23c]);

    }

}

