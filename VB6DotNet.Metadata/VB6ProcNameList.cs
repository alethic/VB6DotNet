using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Represents a list of procedure names.
    /// </summary>
    public class VB6ProcNameList : IEnumerable<string>, IEnumerable, IReadOnlyList<string>
    {

        readonly PEReader pe;
        readonly int start;
        readonly int count;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        internal VB6ProcNameList(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of procedure names.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the procedure name at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index] => index < count ? ReadAbsoluteCString(BinaryPrimitives.ReadInt32LittleEndian(pe.ToSpan(start + index * 4, 4))) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<string> IEnumerable<string>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<string>, IEnumerator
        {

            readonly VB6ProcNameList parent;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="parent"></param>
            internal Enumerator(VB6ProcNameList parent)
            {
                this.parent = parent;

                index = -1;
            }

            /// <summary>
            /// Gets the current method name.
            /// </summary>
            public string Current => index < parent.Count ? parent[index] : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next method name.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return ++index < parent.Count;
            }

            /// <summary>
            /// Resets the enumerator back to the beginning.
            /// </summary>
            public void Reset()
            {
                index = -1;
            }

            /// <summary>
            /// Disposes of the instance.
            /// </summary>
            void IDisposable.Dispose()
            {

            }

            object IEnumerator.Current => Current;

        }

        /// <summary>
        /// Reads a BSTR from the given offset pointer.
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        string ReadAbsoluteCString(int ptr)
        {
            return pe.ToSpan(ptr - (int)pe.PEHeaders.PEHeader.ImageBase).ToStringForCString();
        }

    }

}
