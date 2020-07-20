using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// Represents a list of functions.
    /// </summary>
    readonly struct ExportTableDirectory : IEnumerable<ExportTable>, IEnumerable, IReadOnlyList<ExportTable>
    {

        readonly PEReader pe;
        readonly int start;
        readonly int count;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="start"></param>
        internal ExportTableDirectory(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of entries.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the entry at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ExportTable this[int index] => index < count ? new ExportTable(pe, start) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<ExportTable> IEnumerable<ExportTable>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<ExportTable>, IEnumerator
        {

            readonly ExportTableDirectory parent;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="parent"></param>
            internal Enumerator(ExportTableDirectory parent)
            {
                this.parent = parent;

                index = -1;
            }

            /// <summary>
            /// Gets the current entry.
            /// </summary>
            public ExportTable Current => index < parent.Count ? parent[index] : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next entry.
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
