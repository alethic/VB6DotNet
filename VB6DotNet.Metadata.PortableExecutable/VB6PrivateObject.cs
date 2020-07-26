using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// The Private Object Descriptor Table is pointed by an array defined in the Object List Pointer in the Secondary Project Information. The whole structure can be deleted after compilation.
    /// </summary>
    public readonly struct VB6PrivateObject
    {

        internal const int Size = 64;

        readonly PEReader pe;
        readonly int offset;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6PrivateObject(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets the pointer to the heap link
        /// </summary>
        uint HeapLinkPtr => BinaryPrimitives.ReadUInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Gets the pointer to the object info for this object.
        /// </summary>
        int ObjectInfoPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Gets the object info for this object.
        /// </summary>
        public VB6ObjectInfo ObjectInfo => new VB6ObjectInfo(pe, ObjectInfoPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public ReadOnlySpan<byte> IdeData => Span[0xc..0x18];

        /// <summary>
        /// Pointer to object descriptor pointers.
        /// </summary>
        int ObjectListPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x18..0x1c]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public int IdeData2 => BinaryPrimitives.ReadInt32LittleEndian(Span[0x1c..0x20]);

        /// <summary>
        /// Pointer to object descriptor pointers.
        /// </summary>
        int ObjectList2Ptr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x20..0x2c]);

        /// <summary>
        /// Not valid after compilation.
        /// </summary>
        public ReadOnlySpan<byte> IdeData3 => Span[0x2c..0x38];

        /// <summary>
        /// Type of the object described.
        /// </summary>
        public int ObjectType => BinaryPrimitives.ReadInt32LittleEndian(Span[0x38..0x3c]);

        /// <summary>
        /// Template version of structure. 
        /// </summary>
        public int Identifier => BinaryPrimitives.ReadInt32LittleEndian(Span[0x3c..0x40]);

    }

}

