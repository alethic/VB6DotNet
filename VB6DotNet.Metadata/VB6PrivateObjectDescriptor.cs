using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// The Private Object Descriptor Table is pointed by an array defined in the Object List Pointer in the Secondary Project Information. The whole structure can be deleted after compilation.
    /// </summary>
    public readonly ref struct VB6PrivateObjectDescriptor
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6PrivateObjectDescriptor(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 64);
        }

        /// <summary>
        /// Heap link.
        /// </summary>
        public uint HeapLinkPtr => BinaryPrimitives.ReadUInt32LittleEndian(memory[0x0..0x4]);

        /// <summary>
        /// Reference to the object info for this Object.
        /// </summary>
        public VB6ObjectInfo ObjectInfo => new VB6ObjectInfo(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x4..0x8]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(memory[0x8..0xc]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public ReadOnlySpan<byte> IdeData => memory[0xc..0x18];

        /// <summary>
        /// Pointer to object descriptor pointers.
        /// </summary>
        public int ObjectListPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x18..0x1c]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public int IdeData2 => BinaryPrimitives.ReadInt32LittleEndian(memory[0x1c..0x20]);

        /// <summary>
        /// Pointer to object descriptor pointers.
        /// </summary>
        public int ObjectList2Ptr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x20..0x2c]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public ReadOnlySpan<byte> IdeData3 => memory[0x2c..0x38];

        /// <summary>
        /// Type of the object described.
        /// </summary>
        public int ObjectType => BinaryPrimitives.ReadInt32LittleEndian(memory[0x38..0x3c]);

        /// <summary>
        /// Template version of structure. 
        /// </summary>
        public int Identifier => BinaryPrimitives.ReadInt32LittleEndian(memory[0x3c..0x40]);

    }

}

