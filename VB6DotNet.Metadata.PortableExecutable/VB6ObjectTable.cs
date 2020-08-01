using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// The Object Table structure is pointed by the Project Info Structure. It contains points to the Object Array, as well as more repeated Project Data (presumably so it can be read quickly from here). Some values are also only used when running the project in memory(in the IDE). 
    /// </summary>
    public readonly struct VB6ObjectTable
    {

        internal const int Size = 84;

        readonly PEReader pe;
        readonly int offset;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ObjectTable(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets the pointer to the heap link.
        /// </summary>
        uint HeapLinkPtr => BinaryPrimitives.ReadUInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Pointer to VB project exec COM object.
        /// </summary>
        uint ExecProjPtr => BinaryPrimitives.ReadUInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Gets the pointer to the secondary project info.
        /// </summary>
        int ProjectInfo2Ptr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Gets the secondary project info.
        /// </summary>
        public VB6ProjectInfo2 ProjectInfo2 => new VB6ProjectInfo2(pe, ProjectInfo2Ptr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        int Reserved => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Not used in compiled mode.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// Pointer to the project object.
        /// </summary>
        int ProjectObjectPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x14..0x18]);

        /// <summary>
        /// Gets the project object.
        /// </summary>
        public VB6ProjectInfo ProjectObject => new VB6ProjectInfo(pe, ProjectObjectPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// GUID of the Object Table.
        /// </summary>
        public Guid UuidObject => GuidExtensions.ToGuid(Span[0x18..0x28]);

        /// <summary>
        /// Internal flag used during compilation.
        /// </summary>
        ushort CompileState => BinaryPrimitives.ReadUInt16LittleEndian(Span[0x28..0x2a]);

        /// <summary>
        /// Total objects present in Project.
        /// </summary>
        public short ObjectsCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x2a..0x2c]);

        /// <summary>
        /// Total objects compiled in Project.
        /// </summary>
        public short ObjectsCompiledCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x2c..0x2e]);

        /// <summary>
        /// Total objects in use.
        /// </summary>
        public short ObjectsInUseCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x2e..0x30]);

        /// <summary>
        /// Pointer to the first object.
        /// </summary>
        int ObjectsPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x30..0x34]);

        /// <summary>
        /// Gets the collection of objects.
        /// </summary>
        public VB6ObjectList Objects => ObjectsPtr != 0 ? new VB6ObjectList(pe, ObjectsPtr - (int)pe.PEHeaders.PEHeader.ImageBase, ObjectsCount) : null;

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeFlag => BinaryPrimitives.ReadInt32LittleEndian(Span[0x34..0x38]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData => BinaryPrimitives.ReadInt32LittleEndian(Span[0x38..0x3c]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData2 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x3c..0x40]);

        /// <summary>
        /// Pointer to Project name.
        /// </summary>
        public string ProjectName => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x40..0x44]));

        /// <summary>
        /// LCID of Project.
        /// </summary>
        public int Lcid => BinaryPrimitives.ReadInt32LittleEndian(Span[0x44..0x48]);

        /// <summary>
        /// Alternate LCID of Project.
        /// </summary>
        public int Lcid2 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x48..0x4c]);

        /// <summary>
        /// Flag/Pointer used in IDE only.
        /// </summary>
        public int IdeData3 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4c..0x50]);

        /// <summary>
        /// Tempalte version of structure.
        /// </summary>
        public int Identifier => BinaryPrimitives.ReadInt32LittleEndian(Span[0x50..0x54]);

        /// <summary>
        /// Reads a BSTR from the given offset pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        string ReadAbsoluteCString(int ptr)
        {
            return pe.ToSpan(ptr - (int)pe.PEHeaders.PEHeader.ImageBase).ToStringForCString();
        }

    }

}
