using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace VB6DotNet.PortableExecutable
{

    /// <summary>
    /// Represents a list of <see cref="VB6Object"/>s.
    /// </summary>
    public class VB6ObjectList : IEnumerable<VB6Object>, IEnumerable, IReadOnlyList<VB6Object>
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
        internal VB6ObjectList(PEReader pe, int start, int count)
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
        public VB6Object this[int index] => index < count ? new VB6Object(pe, start + index * VB6Object.Size) : throw new IndexOutOfRangeException();

        /// <summary>
        /// Gets an enumerator.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(pe, start, count);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<VB6Object> IEnumerable<VB6Object>.GetEnumerator() => GetEnumerator();

        public struct Enumerator : IEnumerator<VB6Object>, IEnumerator
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
            public VB6Object Current => index < count ? new VB6Object(pe, start + index * VB6Object.Size) : throw new InvalidOperationException();

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
