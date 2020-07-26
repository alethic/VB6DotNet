using System.Collections.Generic;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes a list of arguments following an opcode.
    /// </summary>
    public class OpArgList : List<OpArg>
    {

        readonly OpDescriptorArgList descriptors;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptors"></param>
        /// <param name="data"></param>
        public OpArgList(OpDescriptorArgList descriptors) : base()
        {
            this.descriptors = descriptors;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptors"></param>
        /// <param name="data"></param>
        public OpArgList(OpDescriptorArgList descriptors, params OpArg[] args) : base(args)
        {
            this.descriptors = descriptors;
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