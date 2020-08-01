﻿using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;
using System.Text;

using VB6DotNet.Metadata.PortableExecutable.Exports;
using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
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
        /// <param name="pe"></param>
        internal VB6MetadataReader(PEReader pe)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
        }

        /// <summary>
        /// Gets teh associated <see cref="PEReader"/>.
        /// </summary>
        public PEReader PortableExecutable => pe;

        /// <summary>
        /// Gets the project info structure.
        /// </summary>
        public VB6ExeProjectInfo ProjectInfo => new VB6ExeProjectInfo(pe, GetExeProjectInfoAddress());

        /// <summary>
        /// Gets the VB Project Info structure data offset.
        /// </summary>
        /// <returns></returns>
        int GetExeProjectInfoAddress()
        {
            if (pe.PEHeaders.IsDll)
                return GetExeProjectInfoAddressOffsetForLibrary();

            if (pe.PEHeaders.IsExe)
                return GetExeProjectInfoAddressOffsetForExecutable();

            throw new BadImageFormatException();
        }

        /// <summary>
        /// Gets the offset within the PE of the VB project info structure for an executable.
        /// </summary>
        /// <returns></returns>
        int GetExeProjectInfoAddressOffsetForExecutable()
        {
            var ep = pe.PEHeaders.PEHeader.AddressOfEntryPoint;
            var rd = pe.ToSpan(ep, 5);

            // first byte should be push instruction with offset as argument
            var i1 = rd[0];
            if (i1 != 0x68) // PUSH
                throw new BadImageFormatException("Expected PUSH instruction at entry point. Executable might not be a VB6 executable.");

            // argument should be 32 bits
            var of = BinaryPrimitives.ReadInt32LittleEndian(rd.Slice(1, 4));
            if (of == 0)
                throw new BadImageFormatException("Unexpected null offset locating project info offset. Executable might not be a VB6 executable.");

            // address offset is from image base
            return of - (int)pe.PEHeaders.PEHeader.ImageBase;
        }

        /// <summary>
        /// Gets the offset within the PE of the VB project info structure for a library.
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        int GetExeProjectInfoAddressOffsetForLibrary()
        {
            var ed = pe.ReadExportTableDirectory();
            if (ed.Count == 0)
                throw new BadImageFormatException("Could not locate export table directory. Executable might not be a VB6 library.");

            var et = ed[0];
            foreach (var funcName in new[] { "DllCanUnloadNow", "DllGetClassObject", "DllRegisterServer", "DllUnregisterServer" })
            {
                // find named export
                var o = -1;
                for (var i = 0; i < et.Names.Count; i++)
                {
                    if (et.Names[i] != funcName)
                        continue;

                    // found index
                    o = i;
                    break;
                }

                // did not find export
                if (o < 0)
                    break;

                // must be a symbol
                var e = et.Exports[et.Ordinals[o]];
                if (e.Type != ExportType.Symbol)
                    break;

                // search first bit of function for push instruction and take data
                var c = pe.ToSpan(e.Symbol, 8);
                for (var i = 0; i < 8; i++)
                {
                    if (c[i] != 0x68)
                        continue;

                    // arg should be pointer to header with magic value
                    var h = BinaryPrimitives.ReadInt32LittleEndian(c.Slice(i + 1, 4)) - (int)pe.PEHeaders.PEHeader.ImageBase;
                    var s = Encoding.ASCII.GetString(pe.ToSpan(h, 4));
                    if (s == VB6ExeProjectInfo.Magic)
                        return h;
                }
            }

            throw new BadImageFormatException("Could not locate export table directory. Executable might not be a VB6 library.");
        }

    }

}
