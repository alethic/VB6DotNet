using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.PortableExecutable.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable.Exports
{

    /// <summary>
    /// Represents a list of functions.
    /// </summary>
    readonly struct ExportList : IEnumerable<Export>, IEnumerable, IReadOnlyList<Export>
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
        internal ExportList(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of functions.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the functions at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Export this[int index] => index < count ? new Export(pe, start + index * 4) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<Export> IEnumerable<Export>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<Export>, IEnumerator
        {

            readonly ExportList parent;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="parent"></param>
            internal Enumerator(ExportList parent)
            {
                this.parent = parent;

                index = -1;
            }

            /// <summary>
            /// Gets the current function.
            /// </summary>
            public Export Current => index < parent.Count ? parent[index] : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next function.
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
