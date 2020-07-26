using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// The Object Information structure defines an Object and provides various information to its methods and constants (in Pseudo Code).
    /// </summary>
    public readonly struct VB6ObjectInfo
    {

        internal const int Size = 56;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ObjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Always 1 after compilation.
        /// </summary>
        public short RefCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x0..0x2]);

        /// <summary>
        /// Index of this object.
        /// </summary>
        public short ObjectIndex => BinaryPrimitives.ReadInt16LittleEndian(Span[0x2..0x4]);

        /// <summary>
        /// Pointer to the object table.
        /// </summary>
        int ObjectTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Gets the object table.
        /// </summary>
        public VB6ObjectTable ObjectTable => new VB6ObjectTable(pe, ObjectTablePtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Zero after compilation. Used in IDE only.
        /// </summary>
        public int IdeData => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Pointer to the private object descriptor. 
        /// </summary>
        int PrivateObjectPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Gets the private object descriptor.
        /// </summary>
        public VB6PrivateObject PrivateObject => new VB6PrivateObject(pe, PrivateObjectPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Always -1 after compilation.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// Unused.
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(Span[0x14..0x18]);

        /// <summary>
        /// Pointer to the object descriptor.
        /// </summary>
        int ObjectPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x18..0x1c]);

        /// <summary>
        /// Reference to object descriptor.
        /// </summary>
        public VB6Object Object => new VB6Object(pe, ObjectPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Pointer to the project data.
        /// </summary>
        int ProjectDataPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x1c..0x20]);

        /// <summary>
        /// Gets the project data.
        /// </summary>
        public VB6ProjectInfo ProjectData => new VB6ProjectInfo(pe, ProjectDataPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Gets the number of procedures on the object.
        /// </summary>
        public short ProcedureCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x20..0x22]);

        /// <summary>
        /// Zeroed out after compilation. IDE only.
        /// </summary>
        public short ProcedureCount2 => BinaryPrimitives.ReadInt16LittleEndian(Span[0x22..0x24]);

        /// <summary>
        /// Pointer to pointers to the procedures associated with the object.
        /// </summary>
        int ProceduresPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x24..0x28]);

        /// <summary>
        /// Gets the procedures associated with the object.
        /// </summary>
        public VB6ProcDscInfoList Procedures => new VB6ProcDscInfoList(pe, ProceduresPtr - (int)pe.PEHeaders.PEHeader.ImageBase, ProcedureCount);

        /// <summary>
        /// Number of constants in constant pool. 
        /// </summary>
        public short Constants => BinaryPrimitives.ReadInt16LittleEndian(Span[0x28..0x2a]);

        /// <summary>
        /// Constants to allocate in constants pool. 
        /// </summary>
        public short MaxConstants => BinaryPrimitives.ReadInt16LittleEndian(Span[0x2a..0x2c]);

        /// <summary>
        /// Valid in IDE only.
        /// </summary>
        int IdeData2Ptr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x2c..0x30]);

        /// <summary>
        /// Valid in IDE only.
        /// </summary>
        int IdeData3Ptr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x30..0x34]);

        /// <summary>
        /// Pointer to constants pool.
        /// </summary>
        int ConstantsPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x34..0x38]);

    }

}

