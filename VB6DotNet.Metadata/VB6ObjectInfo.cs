using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// The Object Information structure defines an Object and provides various information to its methods and constants (in Pseudo Code).
    /// </summary>
    public readonly ref struct VB6ObjectInfo
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ObjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 56);
        }

        /// <summary>
        /// Always 1 after compilation.
        /// </summary>
        public short RefCount => BinaryPrimitives.ReadInt16LittleEndian(memory[0x0..0x2]);

        /// <summary>
        /// Index of this object.
        /// </summary>
        public short ObjectIndex => BinaryPrimitives.ReadInt16LittleEndian(memory[0x2..0x4]);

        /// <summary>
        /// Pointer to the object table.
        /// </summary>
        public VB6ObjectTable ObjectTable => new VB6ObjectTable(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x4..0x8]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Zero after compilation. Used in IDE only.
        /// </summary>
        public int IdeData => BinaryPrimitives.ReadInt32LittleEndian(memory[0x8..0xc]);

        /// <summary>
        /// Pointer to the private object descriptor. 
        /// </summary>
        public VB6PrivateObjectDescriptor PrivateObject => new VB6PrivateObjectDescriptor(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0xc..0x10]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Always -1 after compilation.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(memory[0x10..0x14]);

        /// <summary>
        /// Unused.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(memory[0x14..0x18]);

        /// <summary>
        /// Reference to public object descriptor.
        /// </summary>
        public VB6PublicObjectDescriptor Object => new VB6PublicObjectDescriptor(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x18..0x1c]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Reference to in-memory project object.
        /// </summary>
        public VB6ProjectData ProjectData => new VB6ProjectData(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x1c..0x20]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Number of Methods.
        /// </summary>
        public short MethodCount => BinaryPrimitives.ReadInt16LittleEndian(memory[0x20..0x22]);

        /// <summary>
        /// Zeroed out after compilation. IDE only.
        /// </summary>
        public short MethodCount2 => BinaryPrimitives.ReadInt16LittleEndian(memory[0x22..0x24]);

        /// <summary>
        /// Pointer to Array of Methods.
        /// </summary>
        public int MethodsPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x24..0x28]);

        /// <summary>
        /// Number of constants in constant pool. 
        /// </summary>
        public short Constants => BinaryPrimitives.ReadInt16LittleEndian(memory[0x28..0x2a]);

        /// <summary>
        /// Constants to allocate in constants pool. 
        /// </summary>
        public short MaxConstants => BinaryPrimitives.ReadInt16LittleEndian(memory[0x2a..0x2c]);

        /// <summary>
        /// Valid in IDE only.
        /// </summary>
        public int IdeData2Ptr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x2c..0x30]);

        /// <summary>
        /// Valid in IDE only.
        /// </summary>
        public int IdeData3Ptr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x30..0x34]);

        /// <summary>
        /// Pointer to constants pool.
        /// </summary>
        public int ConstantsPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x34..0x38]);

    }

}

