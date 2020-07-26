using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// The COM Registration Data contains information used if the image file is ActiveX, and contains valuable COM Registration data such as Typelib information, Designer data and Interface CLSIDs.
    /// </summary>
    public readonly struct VB6RegData
    {

        internal const int Size = 42;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6RegData(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets the pointer to the COM registration info.
        /// </summary>
        int RegInfoPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Gets the COM registration info.
        /// </summary>
        public VB6RegInfo RegInfo => new VB6RegInfo(pe, (RegInfoPtr - (int)pe.PEHeaders.PEHeader.ImageBase), offset);

        /// <summary>
        /// Project/TypeLib name.
        /// </summary>
        int ProjectNamePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Help directory.
        /// </summary>
        int HelpDirectoryPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Project description.
        /// </summary>
        int ProjectDescriptionPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// CLSID of Project/TypeLib.
        /// </summary>
        public Guid ProjectClsId => new Guid(Span[0x10..0x20]);

        /// <summary>
        /// LCID of type library.
        /// </summary>
        public int TlbLcid => BinaryPrimitives.ReadInt32LittleEndian(Span[0x20..0x24]);

        /// <summary>
        /// Unknown value.
        /// </summary>
        short Unknown => BinaryPrimitives.ReadInt16LittleEndian(Span[0x24..0x26]);

        /// <summary>
        /// Typelib major version.
        /// </summary>
        public short TlbVerMajor => BinaryPrimitives.ReadInt16LittleEndian(Span[0x26..0x28]);

        /// <summary>
        /// Typelib minor version.
        /// </summary>
        public short TlbVerMinor => BinaryPrimitives.ReadInt16LittleEndian(Span[0x28..0x2a]);

    }

}

