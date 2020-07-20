using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    public readonly struct VB6ExeProjectInfo
    {

        internal const int Size = 104;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal unsafe VB6ExeProjectInfo(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;

            if (Signature != "VB5!")
                throw new BadImageFormatException("Image is not a VB6 portable executable.");
        }

        ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Gets a string containing the VB magic number.
        /// </summary>
        public string Signature => Encoding.ASCII.GetString(Span[0x0..0x4]);

        /// <summary>
        /// Build of the VB6 runtime.
        /// </summary>
        public ushort RuntimeBuild => BinaryPrimitives.ReadUInt16LittleEndian(Span[0x4..0x6]);

        /// <summary>
        /// Language DLL.
        /// </summary>
        public string LanguageDll => Span[0x6..0x14].ToStringForCString();

        /// <summary>
        /// Backup language DLL.
        /// </summary>
        public string BackupLanguageDll => Span[0x14..0x22].ToStringForCString();

        /// <summary>
        /// Runtime DLL version.
        /// </summary>
        public ushort RuntimeDllVersion => BinaryPrimitives.ReadUInt16LittleEndian(Span[0x22..0x24]);

        /// <summary>
        /// LCID of language DLL.
        /// </summary>
        public int Lcid => BinaryPrimitives.ReadInt32LittleEndian(Span[0x24..0x28]);

        /// <summary>
        /// LCID of backup language DLL.
        /// </summary>
        public int BackupLcid => BinaryPrimitives.ReadInt32LittleEndian(Span[0x28..0x2c]);

        /// <summary>
        /// Pointer to the main code.
        /// </summary>
        uint SubMainPtr => BinaryPrimitives.ReadUInt32LittleEndian(Span[0x2c..0x30]);

        /// <summary>
        /// Gets the pointer to the project info.
        /// </summary>
        int ProjectInfoPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x30..0x34]);

        /// <summary>
        /// Gets the project info.
        /// </summary>
        public VB6ProjectInfo ProjectInfo => new VB6ProjectInfo(pe, ProjectInfoPtr - (int)pe.PEHeaders.PEHeader.ImageBase);

        /// <summary>
        /// Control flags.
        /// </summary>
        public VB6ExeProjectInfoControlFlags1 ControlFlags1 => (VB6ExeProjectInfoControlFlags1)BinaryPrimitives.ReadUInt32LittleEndian(Span[0x34..0x38]);

        /// <summary>
        /// Second control flags.
        /// </summary>
        public VB6ExeProjectInfoControlFlags2 ControlFlags2 => (VB6ExeProjectInfoControlFlags2)BinaryPrimitives.ReadUInt32LittleEndian(Span[0x38..0x3c]);

        /// <summary>
        /// Thread flags.
        /// </summary>
        public VB6ExeProjectInfoThreadFlags ThreadFlags => (VB6ExeProjectInfoThreadFlags)BinaryPrimitives.ReadUInt32LittleEndian(Span[0x3c..0x40]);

        /// <summary>
        /// Count of threads to support in pool.
        /// </summary>
        public int ThreadCount => BinaryPrimitives.ReadInt32LittleEndian(Span[0x40..0x44]);

        /// <summary>
        /// Number of forms.
        /// </summary>
        public short FormCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x44..0x46]);

        /// <summary>
        /// Number of external controls.
        /// </summary>
        public short ExternalCount => BinaryPrimitives.ReadInt16LittleEndian(Span[0x46..0x48]);

        /// <summary>
        /// Number of thunks to create.
        /// </summary>
        public int ThunkCount => BinaryPrimitives.ReadInt32LittleEndian(Span[0x48..0x4c]);

        /// <summary>
        /// Pointer to GUI Table.
        /// </summary>
        int GuiTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4c..0x50]);

        /// <summary>
        /// Pointer to External Table.
        /// </summary>
        int ExternalTablePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x50..0x54]);

        /// <summary>
        /// Pointer to COM Information.
        /// </summary>
        int ComRegisterDataPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x54..0x58]);

        /// <summary>
        /// Gets the project description.
        /// </summary>
        public string ProjectDescription => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x58..0x5c]));

        /// <summary>
        /// Gets the name of the project EXE.
        /// </summary>
        public string ProjectExeName => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x5c..0x60]));

        /// <summary>
        /// Gets the name of the project help file.
        /// </summary>
        public string ProjectHelpFileName => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x60..0x64]));

        /// <summary>
        /// Gets the project name.
        /// </summary>
        public string ProjectName => ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(Span[0x64..0x68]));

        /// <summary>
        /// Reads a C-style string offset from the header position.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        string ReadRelativeCString(int ptr)
        {
            return pe.ToSpan(offset + ptr).ToStringForCString();
        }

    }

}
