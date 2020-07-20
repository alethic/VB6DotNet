using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    readonly struct Export
    {

        internal const int Size = 4;

        readonly int offset;
        readonly PEReader pe;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        internal Export(PEReader pe, int offset)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Returns <c>true</c> if the RVA is within the export table directory.
        /// </summary>
        /// <param name="rva"></param>
        /// <returns></returns>
        bool IsForwarderRva(int rva)
        {
            if (rva >= pe.PEHeaders.PEHeader.ExportTableDirectory.RelativeVirtualAddress &&
                rva <= pe.PEHeaders.PEHeader.ExportTableDirectory.RelativeVirtualAddress + pe.PEHeaders.PEHeader.ExportTableDirectory.Size - 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the type of export.
        /// </summary>
        public ExportType Type => IsForwarderRva(BinaryPrimitives.ReadInt32LittleEndian(Span)) ? ExportType.Forwarder : ExportType.Symbol;

        /// <summary>
        /// The address of the exported symbol when loaded into memory, relative to the image base. For example, the address of an exported function.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public int Symbol => Type == ExportType.Symbol ? BinaryPrimitives.ReadInt32LittleEndian(Span) : throw new InvalidOperationException();

        /// <summary>
        /// This string gives the DLL name and the name of the export (for example, "MYDLL.expfunc") or the DLL name and the ordinal number of the export (for example, "MYDLL.#27").
        /// </summary>z
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public string Forwarder => Type == ExportType.Forwarder ? pe.ToSpan(BinaryPrimitives.ReadInt32LittleEndian(Span)).ToStringForCString() : throw new InvalidOperationException();

    }

}

