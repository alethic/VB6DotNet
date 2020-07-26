using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes a list of arguments following an opcode.
    /// </summary>
    public readonly ref struct OpRefArgList
    {

        /// <summary>
        /// Providers an iterator over the argument reference list.
        /// </summary>
        public ref struct Iterator
        {

            readonly OpRefArgList args;
            int index;
            int start;

            /// <summary>
            /// Initializes a new instance.
            /// </summary>
            /// <param name="args"></param>
            internal Iterator(OpRefArgList args)
            {
                this.args = args;
                index = -1;
                start = -1;
            }

            /// <summary>
            /// Moves to the next argument.
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                // size of previous entry
                var s = index >= 0 ? args.defs[index].Size : 1;

                // new index will be off the list
                if (index + 1 >= args.defs.Count)
                    return false;

                // change to next index
                index += 1;
                start += s;
                return true;
            }

            /// <summary>
            /// Gets the current argument.
            /// </summary>
            public OpRefArg Current => new OpRefArg(args.defs[index], args.data.Slice(start, args.defs[index].Size));

        }

        readonly OpDescriptorArgList defs;
        readonly ReadOnlySpan<byte> data;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="defs"></param>
        /// <param name="data"></param>
        public OpRefArgList(OpDescriptorArgList defs, ReadOnlySpan<byte> data)
        {
            this.defs = defs;
            this.data = data;
        }

        /// <summary>
        /// Gets the total number of arguments.
        /// </summary>
        public int Count => defs.Count;

        /// <summary>
        /// Gets the argument at the specified index. This requires advancing through the instruction stream.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public OpRefArg this[int index] => GetArg(index);

        /// <summary>
        /// Gets the argument at the specified index. This requires advancing through the instruction stream.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        OpRefArg GetArg(int index)
        {
            // definition says there aren't enough arguments
            if (index >= defs.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            var i = -1;
            foreach (var e in this)
                if (++i == index)
                    return e;

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <summary>
        /// Iterates through the arguments.
        /// </summary>
        /// <returns></returns>
        public Iterator GetEnumerator()
        {
            return new Iterator(this);
        }

        /// <summary>
        /// Returns a string representation of this argument reference list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var s = new string[Count];
            var i = 0;
            foreach (var a in this)
                s[i++] = a.ToString();

            return string.Join(", ", s);
        }

    }

}