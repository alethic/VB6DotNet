using System;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    /// <summary>
    /// Describes a VB6 PCode stream.
    /// </summary>
    public readonly struct VB6ProcCode
    {

        readonly PEReader pe;
        readonly int offset;
        readonly int length;

        /// <summary>
        /// Initializes a new instnace.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        internal VB6ProcCode(PEReader pe, int offset, int length)
        {
            this.pe = pe;
            this.offset = offset;
            this.length = length;
        }

        /// <summary>
        /// Gets the P-Code data range.
        /// </summary>
        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, length);

    }

}
