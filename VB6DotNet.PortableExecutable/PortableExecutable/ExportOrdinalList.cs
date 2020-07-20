using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable.PortableExecutable
{

    /// <summary>
    /// Represents a list of export ordinals.
    /// </summary>
    readonly struct ExportOrdinalList : IEnumerable<short>, IEnumerable, IReadOnlyList<short>
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
        internal ExportOrdinalList(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of ordinals.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the ordinal at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public short this[int index] => index < count ? BinaryPrimitives.ReadInt16LittleEndian(pe.ToSpan(start + index * 2, 2)) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<short> IEnumerable<short>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<short>, IEnumerator
        {

            readonly ExportOrdinalList parent;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="parent"></param>
            internal Enumerator(ExportOrdinalList parent)
            {
                this.parent = parent;

                index = -1;
            }

            /// <summary>
            /// Gets the current ordinal.
            /// </summary>
            public short Current => index < parent.Count ? parent[index] : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next ordinal.
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

    }

}
