using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable.PortableExecutable
{

    /// <summary>
    /// Represents a list of procedure names.
    /// </summary>
    readonly struct ExportNameList : IEnumerable<string>, IEnumerable, IReadOnlyList<string>
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
        internal ExportNameList(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of names.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the name at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[int index] => index < count ? ReadRelativeCString(BinaryPrimitives.ReadInt32LittleEndian(pe.ToSpan(start + index * 4, 4))) : throw new IndexOutOfRangeException();

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

            readonly ExportNameList parent;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="parent"></param>
            internal Enumerator(ExportNameList parent)
            {
                this.parent = parent;

                index = -1;
            }

            /// <summary>
            /// Gets the current name.
            /// </summary>
            public string Current => index < parent.Count ? parent[index] : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next name.
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
        string ReadRelativeCString(int ptr)
        {
            return pe.ToSpan(ptr).ToStringForCString();
        }

    }

}
