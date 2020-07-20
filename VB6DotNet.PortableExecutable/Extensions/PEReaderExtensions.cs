using System;
using System.Reflection.PortableExecutable;

namespace VB6DotNet.PortableExecutable.Extensions
{

    /// <summary>
    /// Provides extension methods for working with a <see cref="PEReader"/>.
    /// </summary>
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

        /// <summary>
        /// Gets a <see cref="Span{T}"/> covering the entire PE image.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public unsafe static ReadOnlySpan<byte> ToSpan(this PEReader self, int start, int count)
        {
            return ToSpan(self)[start..(start + count)];
        }

        /// <summary>
        /// Gets a <see cref="Span{T}"/> covering the entire PE image.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public unsafe static ReadOnlySpan<byte> ToSpan(this PEReader self, int start)
        {
            return ToSpan(self)[start..];
        }

    }

}
