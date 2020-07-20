using System;
using System.Text;

namespace VB6DotNet.PortableExecutable.Extensions
{

    static class ReadOnlySpanExtensions
    {

        /// <summary>
        /// Gets the length of a C-style string. If no NULL is found, returns the total length of the span.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        static int LengthOfCString(ReadOnlySpan<byte> self)
        {
            for (int i = 0; i < self.Length; i++)
                if (self[i] == 0x00)
                    return i;

            return self.Length;
        }

        /// <summary>
        /// Parses the C-style string within the span into a string.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToStringForCString(this ReadOnlySpan<byte> self)
        {
            return Encoding.ASCII.GetString(self.Slice(0, LengthOfCString(self)));
        }

    }

}
