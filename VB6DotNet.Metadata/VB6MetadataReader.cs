using System;
using System.Reflection.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Reads metadata from a Visual Basic 6 portable executable.
    /// </summary>
    public class VB6MetadataReader
    {

        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        internal VB6MetadataReader(PEReader pe)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
        }

        /// <summary>
        /// Gets the project info structure.
        /// </summary>
        public VB6ExeProjectInfo ProjectInfo => new VB6ExeProjectInfo(pe, GetExeProjectInfoAddress(pe.GetEntireImage()));

        /// <summary>
        /// Gets the VB Project Info structure data offset.
        /// </summary>
        /// <returns></returns>
        int GetExeProjectInfoAddress(PEMemoryBlock mb)
        {
            if (pe.PEHeaders.IsDll)
                return GetExeProjectInfoAddressOffsetForLibrary(mb);

            if (pe.PEHeaders.IsExe)
                return GetExeProjectInfoAddressOffsetForExecutable(mb);

            throw new BadImageFormatException();
        }

        /// <summary>
        /// Gets the offset within the PE of the VB project info structure for an executable.
        /// </summary>
        /// <returns></returns>
        int GetExeProjectInfoAddressOffsetForExecutable(PEMemoryBlock mb)
        {
            var ep = pe.PEHeaders.PEHeader.AddressOfEntryPoint;
            var rd = mb.GetReader(ep, 10);

            // first byte should be push instruction with offset as argument
            var i1 = rd.ReadByte();
            if (i1 != 0x68) // PUSH
                throw new BadImageFormatException("Expected PUSH instruction at entry point. Executable might not be a VB6 executable.");

            // argument should be 32 bits
            var of = rd.ReadUInt32();
            if (of == 0)
                throw new BadImageFormatException("Unexpected null offset locating project info offset. Executable might not be a VB6 executable.");

            // address offset is from image base
            return (int)(of - pe.PEHeaders.PEHeader.ImageBase);
        }

        int GetExeProjectInfoAddressOffsetForLibrary(PEMemoryBlock mb)
        {
            var sec = pe.GetSectionData(".edata");
            throw new NotImplementedException();
        }

    }

}
