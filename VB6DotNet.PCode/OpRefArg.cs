using System;
using System.Buffers.Binary;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes a operation argument.
    /// </summary>
    public readonly ref struct OpRefArg
    {

        readonly OpDescriptorArg arg;
        readonly ReadOnlySpan<byte> data;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="data"></param>
        public OpRefArg(OpDescriptorArg arg, ReadOnlySpan<byte> data)
        {
            this.arg = arg;
            this.data = data;
        }

        /// <summary>
        /// Converts the <see cref="OpRefArg"/> to a <see cref="OpArg"/>.
        /// </summary>
        /// <returns></returns>
        public OpArg ToOpArg()
        {
            return new OpArg(arg);
        }

        /// <summary>
        /// Returns a string representation of this argument reference.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return arg.Type switch
            {
                OpArgType.Inline => $"[{arg.ValueType}] {BitConverter.ToString(data.ToArray()).Replace("-", "")}",
                OpArgType.Constant => $"[{arg.ValueType}] const_{BinaryPrimitives.ReadInt16LittleEndian(data):X}",
                OpArgType.Variable => $"[{arg.ValueType}] var_{-BinaryPrimitives.ReadInt16LittleEndian(data):X}",
                _ => throw new InvalidOperationException(),
            };
        }

    }

}