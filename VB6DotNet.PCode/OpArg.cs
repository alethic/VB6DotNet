using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes a operation argument.
    /// </summary>
    public class OpArg
    {

        readonly OpDescriptorArg descriptor;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="descriptor"></param>
        public OpArg(OpDescriptorArg descriptor)
        {
            this.descriptor = descriptor;
        }

        /// <summary>
        /// Gets the descriptor of the argument.
        /// </summary>
        public OpDescriptorArg Descriptor => descriptor;

        /// <summary>
        /// Get sthe source of the argument.
        /// </summary>
        public OpArgType Type => descriptor.Type;

        /// <summary>
        /// Gets the type of the value of the argument.
        /// </summary>
        public OpArgValueType ValueType => descriptor.ValueType;

        /// <summary>
        /// Returns a string representation of this argument reference.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            throw new NotImplementedException();

            //return arg.Source switch
            //{
            //    OpCodeArgSource.Inline => $"[{arg.Type}] {BitConverter.ToString(data.ToArray()).Replace("-", "")}",
            //    OpCodeArgSource.Constant => $"[{arg.Type}] loc_{BinaryPrimitives.ReadInt16LittleEndian(data):X}",
            //    OpCodeArgSource.Variable => $"[{arg.Type}] var_{-BinaryPrimitives.ReadInt16LittleEndian(data):X}",
            //    _ => throw new InvalidOperationException(),
            //};
        }

    }

}