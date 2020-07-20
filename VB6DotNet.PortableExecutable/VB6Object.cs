using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    /// <summary>
    /// The Public Object Descriptor Table is pointed by the Array lpObjectArray in the Object Table. Each Object in the project will have its own. Unlike the private structure, this one is actually used by VB for a variety of tasks.
    /// </summary>
    public readonly struct VB6Object
    {

        internal const int Size = 48;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal VB6Object(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets the pointer to the info for this object.
        /// </summary>
        int ObjectInfoPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Gets the info for this object.
        /// </summary>
        public VB6ObjectInfo ObjectInfo => new VB6ObjectInfo(pe, ObjectInfoPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Gets the optional object info if available.
        /// </summary>
        public VB6OptionalObjectInfo OptionalObjectInfo => ObjectType.HasFlag(VB6ObjectTypeFlags.HasOptionalInfo) ? new VB6OptionalObjectInfo(pe, offset + Size) : throw new InvalidOperationException();

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Pointer to public variable size integers. 
        /// </summary>
        int PublicBytesPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Pointer to public variable size integers. 
        /// </summary>
        public VB6PublicBytes PublicBytes => new VB6PublicBytes(pe, PublicBytesPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Pointer to static variable size integers.
        /// </summary>
        int StaticBytesPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Pointer to public variables in the DATA section.
        /// </summary>
        int ModulePublicPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// Pointer to static variables in the DATA section.
        /// </summary>
        int ModuleStaticPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x14..0x18]);

        /// <summary>
        /// Name of the object.
        /// </summary>
        public string ObjectName => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x18..0x1c]));

        /// <summary>
        /// Number of procedures in the object.
        /// </summary>
        int ProcedureCount => BinaryPrimitives.ReadInt32LittleEndian(Span[0x1c..0x20]);

        /// <summary>
        /// If present, pointer to method names array.
        /// </summary>
        int ProcedureNamesPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x20..0x24]);

        /// <summary>
        /// Gets the set of method names.
        /// </summary>
        public VB6ProcNameList ProcedureNames => ProcedureNamesPtr != 0 ? new VB6ProcNameList(pe, ProcedureNamesPtr - (int)pe.PEHeaders.PEHeader.ImageBase, ProcedureCount) : null;

        /// <summary>
        /// Offset to copy static variables.
        /// </summary>
        int StaticVars => BinaryPrimitives.ReadInt32LittleEndian(Span[0x24..0x28]);

        /// <summary>
        /// Flags defining the Object Type.
        /// </summary>
        public VB6ObjectTypeFlags ObjectType => (VB6ObjectTypeFlags)BinaryPrimitives.ReadInt32LittleEndian(Span[0x28..0x2c]);

        /// <summary>
        /// Not valid after compilation. 
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(Span[0x2c..0x30]);

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

