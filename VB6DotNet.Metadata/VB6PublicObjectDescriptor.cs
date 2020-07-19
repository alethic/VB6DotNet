using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// The Public Object Descriptor Table is pointed by the Array lpObjectArray in the Object Table. Each Object in the project will have its own. Unlike the private structure, this one is actually used by VB for a variety of tasks.
    /// </summary>
    public readonly ref struct VB6PublicObjectDescriptor
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6PublicObjectDescriptor(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 64);
        }

        /// <summary>
        /// Pointer to the object info for this object.
        /// </summary>
        public VB6ObjectInfo ObjectInfo => new VB6ObjectInfo(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x0..0x4]) - (uint)pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// Always set to -1 after compiling. Unused.
        /// </summary>
        public int Reserved => BinaryPrimitives.ReadInt32LittleEndian(memory[0x4..0x8]);

        /// <summary>
        /// Pointer to public variable size integers. 
        /// </summary>
        public int PublicBytesPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x8..0xc]);

        /// <summary>
        /// Pointer to static variable size integers.
        /// </summary>
        public int StaticBytesPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0xc..0x10]);

        /// <summary>
        /// Pointer to public variables in the DATA section.
        /// </summary>
        public int ModulePublicPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x10..0x14]);

        /// <summary>
        /// Pointer to static variables in the DATA section.
        /// </summary>
        public int ModuleStaticPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x14..0x18]);

        /// <summary>
        /// Name of the object.
        /// </summary>
        public string ObjectName => ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x18..0x1c]));

        /// <summary>
        /// Number of methods in the object.
        /// </summary>
        public int MethodCount => BinaryPrimitives.ReadInt32LittleEndian(memory[0x1c..0x20]);

        /// <summary>
        /// If present, pointer to method names array.
        /// </summary>
        public int MethodNamesPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x20..0x24]);

        /// <summary>
        /// Offset to copy static variables.
        /// </summary>
        public int StaticVars => BinaryPrimitives.ReadInt32LittleEndian(memory[0x24..0x28]);

        /// <summary>
        /// Flags defining the Object Type.
        /// </summary>
        public int ObjectType => BinaryPrimitives.ReadInt32LittleEndian(memory[0x28..0x2c]);

        /// <summary>
        /// Not valid after compilation. 
        /// </summary>
        public int Null => BinaryPrimitives.ReadInt32LittleEndian(memory[0x2c..0x30]);

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

