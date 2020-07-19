using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    public readonly ref struct VB6ExeProjectInfo
    {

        readonly int offset;
        readonly PEReader pe;
        readonly ReadOnlySpan<byte> memory;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ExeProjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.memory = pe.ToSpan().Slice(offset, 104);

            if (Magic != "VB5!")
                throw new BadImageFormatException("Image is not a VB6 portable executable.");
        }

        /// <summary>
        /// Gets a string containing the VB magic number.
        /// </summary>
        public string Magic => Encoding.ASCII.GetString(memory[..0x4]);

        /// <summary>
        /// Build of the VB6 runtime.
        /// </summary>
        public ushort RuntimeBuild => BinaryPrimitives.ReadUInt16LittleEndian(memory[0x4..0x6]);

        /// <summary>
        /// Language Extension DLL.
        /// </summary>
        public string LangDll => memory[0x6..0x14].ToStringForCString();

        /// <summary>
        /// 2nd Language Extension DLL.
        /// </summary>
        public string SecLangDll => memory[0x14..0x22].ToStringForCString();

        /// <summary>
        /// Internal Runtime Revision.
        /// </summary>
        public ushort RuntimeRevision => BinaryPrimitives.ReadUInt16LittleEndian(memory[0x22..0x24]);

        /// <summary>
        /// LCID of Language DLL.
        /// </summary>
        public int LCID => BinaryPrimitives.ReadInt32LittleEndian(memory[0x24..0x28]);

        /// <summary>
        /// LCID of 2nd Language DLL.
        /// </summary>
        public int SecLCID => BinaryPrimitives.ReadInt32LittleEndian(memory[0x28..0x2c]);

        /// <summary>
        /// Pointer to Sub Main Code.
        /// </summary>
        public uint SubMainPtr => BinaryPrimitives.ReadUInt32LittleEndian(memory[0x2c..0x30]);

        /// <summary>
        /// Gets the project data.
        /// </summary>
        public VB6ProjectData ProjectData => new VB6ProjectData(pe, (int)(BinaryPrimitives.ReadUInt32LittleEndian(memory[0x30..0x34]) - pe.PEHeaders.PEHeader.ImageBase));

        /// <summary>
        /// VB Control Flags for IDs < 32.
        /// </summary>
        public VB6ExeProjectInfoControlFlags1 ControlFlags1 => (VB6ExeProjectInfoControlFlags1)BinaryPrimitives.ReadUInt32LittleEndian(memory[0x34..0x38]);

        /// <summary>
        /// VB Control Flags for IDs > 32.
        /// </summary>
        public VB6ExeProjectInfoControlFlags2 ControlFlags2 => (VB6ExeProjectInfoControlFlags2)BinaryPrimitives.ReadUInt32LittleEndian(memory[0x38..0x3c]);

        /// <summary>
        /// Threading Mode.
        /// </summary>
        public VB6ExeProjectInfoThreadFlags ThreadFlags => (VB6ExeProjectInfoThreadFlags)BinaryPrimitives.ReadUInt32LittleEndian(memory[0x3c..0x40]);

        /// <summary>
        /// Threads to support in pool.
        /// </summary>
        public int ThreadCount => BinaryPrimitives.ReadInt32LittleEndian(memory[0x40..0x44]);

        /// <summary>
        /// Number of forms present.
        /// </summary>
        public short FormCount => BinaryPrimitives.ReadInt16LittleEndian(memory[0x44..0x46]);

        /// <summary>
        /// Number of external controls.
        /// </summary>
        public short ExternalCount => BinaryPrimitives.ReadInt16LittleEndian(memory[0x46..0x48]);

        /// <summary>
        /// Number of thunks to create.
        /// </summary>
        public int ThunkCount => BinaryPrimitives.ReadInt32LittleEndian(memory[0x48..0x4c]);

        /// <summary>
        /// Pointer to GUI Table.
        /// </summary>
        int GuiTablePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x4c..0x50]);

        /// <summary>
        /// Pointer to External Table.
        /// </summary>
        int ExternalTablePtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x50..0x54]);

        /// <summary>
        /// Pointer to COM Information.
        /// </summary>
        int ComRegisterDataPtr => BinaryPrimitives.ReadInt32LittleEndian(memory[0x54..0x58]);

        /// <summary>
        /// Offset to Project Description.
        /// </summary>
        public string ProjectDescription => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x58..0x5c]));

        /// <summary>
        /// Offset to Project EXE Name.
        /// </summary>
        public string ProjectExeName => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x5c..0x60]));

        /// <summary>
        /// Offset to Project Help File.
        /// </summary>
        public string ProjectHelpFile => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x60..0x64]));

        /// <summary>
        /// Offset to Project Name.
        /// </summary>
        public string ProjectName => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(memory[0x64..0x68]));

        /// <summary>
        /// Reads a C-style string offset from the header position.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        unsafe string ReadRelativeCString(int ptr)
        {
            return pe.ToSpan().Slice(offset + ptr).ToStringForCString();
        }

    }

}
