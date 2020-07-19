using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// The Object Table structure is pointed by the Project Info Structure. It contains points to the Object Array, as well as more repeated Project Data (presumably so it can be read quickly from here). Some values are also only used when running the project in memory(in the IDE). 
    /// </summary>
    public readonly ref struct VB6ObjectTable
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ObjectTable(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 84);
        }

        /// <summary>
        /// Heap link.
        /// </summary>
        public uint HeapLinkPtr => BinaryPrimitives.ReadUInt32LittleEndian(memory[0x0..0x4]);

        /// <summary>
        /// Pointer to VB project exec COM object.
        /// </summary>
        public uint ExecProjPtr => BinaryPrimitives.ReadUInt32LittleEndian(memory[0x4..0x8]);

        /// <summary>
        /// Secondary Project Information.
        /// </summary>
        public VB6ProjectInfo2 ProjectInfo2 => new VB6ProjectInfo2(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x8..0xc]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(memory[0xc..0x10]);

        /// <summary>
        /// Not used in compiled mode.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(memory[0x10..0x14]);

        /// <summary>
        /// Pointer to in-memory Project Data.
        /// </summary>
        public VB6ProjectData ProjectObject => new VB6ProjectData(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x14..0x18]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// GUID of the Object Table.
        /// </summary>
        public Guid UuidObject => new Guid(memory[0x18..0x28]);

        /// <summary>
        /// Internal flag used during compilation.
        /// </summary>
        public ushort CompileState => BinaryPrimitives.ReadUInt16LittleEndian(memory[0x28..0x2a]);

        /// <summary>
        /// Total objects present in Project.
        /// </summary>
        public short TotalObjects => BinaryPrimitives.ReadInt16LittleEndian(memory[0x2a..0x2c]);

        /// <summary>
        /// Total objects compiled in Project.
        /// </summary>
        public short CompiledObjects => BinaryPrimitives.ReadInt16LittleEndian(memory[0x2c..0x2e]);

        /// <summary>
        /// Total objects in use.
        /// </summary>
        public short ObjectsInUse => BinaryPrimitives.ReadInt16LittleEndian(memory[0x2e..0x30]);

        /// <summary>
        /// Total objects in use.
        /// </summary>
        public uint ObjectArrayPtr => BinaryPrimitives.ReadUInt32LittleEndian(memory[0x30..0x34]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeFlag => BinaryPrimitives.ReadInt32LittleEndian(memory[0x34..0x38]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData => BinaryPrimitives.ReadInt32LittleEndian(memory[0x38..0x3c]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData2 => BinaryPrimitives.ReadInt32LittleEndian(memory[0x3c..0x40]);

        /// <summary>
        /// Pointer to Project name.
        /// </summary>
        public string ProjectName => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x40..0x44]));

        /// <summary>
        /// LCID of Project.
        /// </summary>
        public int Lcid => BinaryPrimitives.ReadInt32LittleEndian(memory[0x44..0x48]);

        /// <summary>
        /// Alternate LCID of Project.
        /// </summary>
        public int Lcid2 => BinaryPrimitives.ReadInt32LittleEndian(memory[0x48..0x4c]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData3 => BinaryPrimitives.ReadInt32LittleEndian(memory[0x4c..0x50]);

        /// <summary>
        /// Tempalte version of structure.
        /// </summary>
        public int Identifier => BinaryPrimitives.ReadInt32LittleEndian(memory[0x50..0x54]);

        /// <summary>
        /// Reads a BSTR from the given offset pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        unsafe string ReadAbsoluteCString(int ptr)
        {
            return pe.ToSpan().Slice(ptr - (int)pe.PEHeaders.PEHeader.ImageBase).ToStringForCString();
        }

    }

}
