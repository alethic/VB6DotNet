using System;
using System.Reflection.PortableExecutable;

namespace VB6DotNet.Metadata.PortableExecutable
{

    static class PEReaderExtensions
    {

        /// <summary>
        /// Reads the entries from the export directory.
        /// </summary>
        /// <param name="pe"></param>
        /// <returns></returns>
        public static ExportTableDirectory ReadExportTableDirectory(this PEReader pe)
        {
            if (pe is null)
                throw new ArgumentNullException(nameof(pe));

            // return directory table if possible
            if (pe.PEHeaders.PEHeader.ExportTableDirectory.Size > 0 &&
                pe.PEHeaders.TryGetDirectoryOffset(pe.PEHeaders.PEHeader.ExportTableDirectory, out var offset))
                return new ExportTableDirectory(pe, offset, 1);
            else
                return new ExportTableDirectory(pe, 0, 0);
        }

    }

}
