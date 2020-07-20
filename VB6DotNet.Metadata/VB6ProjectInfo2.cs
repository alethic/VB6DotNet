using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// This Secondary structure, pointed by the Object Table contains mostly data used when compiling the project. It does also however pave the way to the Form List (To be described later) and gives the elusive Help Context ID.
    /// </summary>
    public readonly ref struct VB6ProjectInfo2
    {

        internal const int Size = 40;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ProjectInfo2(PEReader pe, int offset)
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
        /// Gets the pointer to the object table.
        /// </summary>
        int ObjectTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Gets the object table.
        /// </summary>
        public VB6ObjectTable ObjectTable => new VB6ObjectTable(pe, ObjectTablePtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Not written or read in any case.
        /// </summary>
        public int Unused => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Gets the pointers to the object list.
        /// </summary>
        int ObjectListPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// Not written or read in any case.
        /// </summary>
        public int Unused2 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x14..0x18]);

        /// <summary>
        /// Gets the project description.
        /// </summary>
        public string ProjectDescription => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x18..0x1c]));

        /// <summary>
        /// Gets the project helpf ile name.
        /// </summary>
        public string ProjectHelpFileName => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x1c..0x20]));

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved2 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x20..0x24]);

        /// <summary>
        /// Gets the help context ID.
        /// </summary>
        public int HelpContextId => BinaryPrimitives.ReadInt32LittleEndian(Span[0x24..0x28]);

        /// <summary>
        /// Reads a BSTR from the given offset pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        unsafe string ReadAbsoluteCString(int ptr)
        {
            return pe.ToSpan(ptr - (int)pe.PEHeaders.PEHeader.ImageBase).ToStringForCString();
        }

    }

}

