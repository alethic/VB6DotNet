using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable.Exports
{

    /// <summary>
    /// Represents the export table within a portable executable. Only one of these should be present.
    /// </summary>
    readonly struct ExportTable
    {

        internal const int Size = 40;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe ExportTable(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        public int Flags => BinaryPrimitives.ReadInt32LittleEndian(Span[0..4]);

        public int Stamp => BinaryPrimitives.ReadInt32LittleEndian(Span[4..8]);

        public short MajorVersion => BinaryPrimitives.ReadInt16LittleEndian(Span[8..10]);

        public short MinorVersion => BinaryPrimitives.ReadInt16LittleEndian(Span[10..12]);

        int NameRva => BinaryPrimitives.ReadInt32LittleEndian(Span[12..16]);

        /// <summary>
        /// Name of the exported library.
        /// </summary>
        public string Name => NameRva != 0 ? ReadRelativeCString(NameRva) : null;

        /// <summary>
        /// Base ordinal bias.
        /// </summary>
        public int OrdinalBase => BinaryPrimitives.ReadInt32LittleEndian(Span[16..20]);

        int ExportsCount => BinaryPrimitives.ReadInt32LittleEndian(Span[20..24]);

        int NamesCount => BinaryPrimitives.ReadInt32LittleEndian(Span[24..28]);

        int ExportsRva => BinaryPrimitives.ReadInt32LittleEndian(Span[28..32]);

        /// <summary>
        /// Exports recorded by the export directory.
        /// </summary>
        public ExportList Exports => new ExportList(pe, ExportsRva, ExportsCount);

        int NamesRva => BinaryPrimitives.ReadInt32LittleEndian(Span[32..36]);

        /// <summary>
        /// These names are the public names through which the symbols are imported and exported; they are not
        /// necessarily the same as the private names that are used within the image file.
        /// </summary>
        public ExportNameList Names => new ExportNameList(pe, NamesRva, NamesCount);

        int OrdinalsRva => BinaryPrimitives.ReadInt32LittleEndian(Span[36..40]);

        /// <summary>
        /// An array of the ordinals that correspond to members of the name pointer table. The correspondence is by
        /// position; therefore, the name pointer table and the ordinal table must have the same number of members.
        /// Each ordinal is an index into the export address table.
        /// </summary>
        public ExportOrdinalList Ordinals => new ExportOrdinalList(pe, OrdinalsRva, NamesCount);

        string ReadRelativeCString(int ptr)
        {
            return pe.ToSpan(ptr).ToStringForCString();
        }

    }

}

