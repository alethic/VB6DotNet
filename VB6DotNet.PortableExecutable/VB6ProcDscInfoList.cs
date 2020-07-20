using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable.Extensions;

namespace VB6DotNet.PortableExecutable
{

    /// <summary>
    /// Represents a list of <see cref="VB6ProcDscInfo"/>s.
    /// </summary>
    public readonly struct VB6ProcDscInfoList : IEnumerable<VB6ProcDscInfo>, IEnumerable, IReadOnlyList<VB6ProcDscInfo>
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
        internal VB6ProcDscInfoList(PEReader pe, int start, int count)
        {
            this.pe = pe;
            this.start = start;
            this.count = count;
        }

        /// <summary>
        /// Gets the count of object descriptors.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Gets the object at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VB6ProcDscInfo this[int index] => index < count ? new VB6ProcDscInfo(pe, BinaryPrimitives.ReadInt32LittleEndian(pe.ToSpan(start + index * 4, 4)) - (int)pe.PEHeaders.PEHeader.ImageBase) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(pe, start, count);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<VB6ProcDscInfo> IEnumerable<VB6ProcDscInfo>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<VB6ProcDscInfo>, IEnumerator
        {

            readonly PEReader pe;
            readonly int start;
            readonly int count;

            int index;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="pe"></param>
            /// <param name="start"></param>
            /// <param name="cur"></param>
            /// <param name="max"></param>
            internal Enumerator(PEReader pe, int start, int count)
            {
                this.pe = pe;
                this.start = start;
                this.count = count;

                index = -1;
            }

            /// <summary>
            /// Gets the current object descriptor.
            /// </summary>
            public VB6ProcDscInfo Current => index < count ? new VB6ProcDscInfo(pe, BinaryPrimitives.ReadInt32LittleEndian(pe.ToSpan(start + index * 4, 4)) - (int)pe.PEHeaders.PEHeader.ImageBase) : throw new InvalidOperationException();

            /// <summary>
            /// Moves to the next object.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                return ++index < count;
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
