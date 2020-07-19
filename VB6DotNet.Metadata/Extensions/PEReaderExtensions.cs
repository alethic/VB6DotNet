using System;
using System.Reflection.PortableExecutable;

namespace VB6DotNet.Metadata.Extensions
{

    static class PEReaderExtensions
    {

        /// <summary>
        /// Gets a <see cref="Span{T}"/> covering the entire PE image.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public unsafe static ReadOnlySpan<byte> ToSpan(this PEReader self)
        {
            var mb = self.GetEntireImage();
            return new ReadOnlySpan<byte>(mb.Pointer, mb.Length);
        }

    }

}
