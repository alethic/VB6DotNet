using System;
using System.Collections.Generic;
using System.Linq;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes an instruction within a P-Code stream.
    /// </summary>
    public class Op
    {

        readonly OpDescriptor descriptor;
        readonly OpArgList args;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptor"></param>
        public Op(OpDescriptor descriptor, OpArgList args)
        {
            this.descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
            this.args = args ?? throw new ArgumentNullException(nameof(args));
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="args"></param>
        public Op(OpDescriptor descriptor, IEnumerable<OpArg> args) :
            this(descriptor, new OpArgList(descriptor.Args, args.ToArray()))
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="args"></param>
        public Op(OpDescriptor descriptor, params OpArg[] args) :
            this(descriptor, new OpArgList(descriptor.Args, args))
        {

        }

        /// <summary>
        /// Gets the descriptor of the operation.
        /// </summary>
        public OpDescriptor Descriptor => descriptor;

        /// <summary>
        /// Gets the code of the operation.
        /// </summary>
        public OpCode Code => descriptor.Code;

        /// <summary>
        /// Gets the set of arguments of the operation.
        /// </summary>
        public OpArgList Args => args;

        /// <summary>
        /// Returns a string representation of this operation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{descriptor.Code} ({args})";
        }

    }

}
