using System;
using System.Collections;
using System.Collections.Generic;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Represents a list of <see cref="OpDescriptorArg"/>s.
    /// </summary>
    public readonly struct OpDescriptorArgList : IReadOnlyList<OpDescriptorArg>
    {

        readonly OpDescriptorArg[] args;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="args"></param>
        public OpDescriptorArgList(OpDescriptorArg[] args)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }

        /// <summary>
        /// Gets the argument at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public OpDescriptorArg this[int index] => ((IReadOnlyList<OpDescriptorArg>)args)[index];

        /// <summary>
        /// Gets the total number of arguments.
        /// </summary>
        public int Count => ((IReadOnlyCollection<OpDescriptorArg>)args).Count;

        /// <summary>
        /// Enumerators through the arguments.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<OpDescriptorArg> GetEnumerator()
        {
            return ((IEnumerable<OpDescriptorArg>)args).GetEnumerator();
        }

        /// <summary>
        /// Enumerators through the arguments.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return args.GetEnumerator();
        }

        /// <summary>
        /// Returns a string representation of the argument list.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(", ", args);
        }

    }

}
