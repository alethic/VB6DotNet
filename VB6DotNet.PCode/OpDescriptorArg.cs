using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes an argument to an <see cref="OpDescriptor"/>.
    /// </summary>
    public readonly struct OpDescriptorArg
    {

        readonly OpArgType type;
        readonly OpArgValueType valueType;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="valueType"></param>
        public OpDescriptorArg(OpArgType type, OpArgValueType valueType)
        {
            this.type = type;
            this.valueType = valueType;
        }

        /// <summary>
        /// Gets the source of the <see cref="OpDescriptorArg"/>.
        /// </summary>
        public OpArgType Type => type;

        /// <summary>
        /// Gets the type of the <see cref="OpDescriptorArg"/>.
        /// </summary>
        public OpArgValueType ValueType => valueType;

        /// <summary>
        /// Gets the size of this argument within the instruction stream.
        /// </summary>
        public int Size => CalculateSize();

        /// <summary>
        /// Calculates the size of this argument within the instruction stream.
        /// </summary>
        int CalculateSize()
        {
            return type switch
            {
                OpArgType.Constant => 2,
                OpArgType.Variable => 2,
                OpArgType.Inline => CalculateArgTypeSize(),
                _ => throw new InvalidOperationException(),
            };
        }

        /// <summary>
        /// Calculates the size of the argument type.
        /// </summary>
        /// <returns></returns>
        int CalculateArgTypeSize()
        {
            return valueType switch
            {
                OpArgValueType.Boolean => 2,
                OpArgValueType.Byte => 1,
                OpArgValueType.Currency => 4,
                OpArgValueType.Date => 8,
                OpArgValueType.Double => 8,
                OpArgValueType.Integer => 2,
                OpArgValueType.Long => 4,
                OpArgValueType.Single => 2,
                OpArgValueType.String => -1,
                OpArgValueType.Variant => 16,
                _ => throw new InvalidOperationException(),
            };
        }

        /// <summary>
        /// Returns a string representation of this argument.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return type switch
            {
                OpArgType.Inline => $"[{valueType}]",
                OpArgType.Constant => $"[{valueType}] const",
                OpArgType.Variable => $"[{valueType}] var",
                _ => throw new InvalidOperationException(),
            };
        }

    }

}