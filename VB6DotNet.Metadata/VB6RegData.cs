using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// The COM Registration Data contains information used if the image file is ActiveX, and contains valuable COM Registration data such as Typelib information, Designer data and Interface CLSIDs.
    /// </summary>
    public readonly ref struct VB6RegData
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6RegData(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 42);
        }

        /// <summary>
        /// Reference to COM Interfaces Info.
        /// </summary>
        public VB6RegInfo RegInfo => new VB6RegInfo(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x0..0x4]) - (uint)pe.PEHeaders.PEHeader.ImageBase), offset);

        /// <summary>
        /// Project/TypeLib name.
        /// </summary>
        public int ProjectNamePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x4..0x8]);

        /// <summary>
        /// Help directory.
        /// </summary>
        public int HelpDirectoryPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x8..0xc]);

        /// <summary>
        /// Project description.
        /// </summary>
        public int ProjectDescriptionPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0xc..0x10]);

        /// <summary>
        /// CLSID of Project/TypeLib.
        /// </summary>
        public Guid ProjectClsId => new Guid(memory[0x10..0x20]);

        /// <summary>
        /// LCID of type library.
        /// </summary>
        public int TlbLcid => BinaryPrimitives.ReadInt32LittleEndian(memory[0x20..0x24]);

        public short Unknown => BinaryPrimitives.ReadInt16LittleEndian(memory[0x24..0x26]);

        /// <summary>
        /// Typelib major version.
        /// </summary>
        public short TlbVerMajor => BinaryPrimitives.ReadInt16LittleEndian(memory[0x26..0x28]);

        /// <summary>
        /// Typelib minor version.
        /// </summary>
        public short TlbVerMinor => BinaryPrimitives.ReadInt16LittleEndian(memory[0x28..0x2a]);

    }

}

