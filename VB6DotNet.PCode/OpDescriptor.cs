using System;
using System.Linq;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes an op code.
    /// </summary>
    public partial class OpDescriptor : IEquatable<OpDescriptor>
    {

        /// <summary>
        /// Returns the <see cref="OpDescriptor"/> for the specified <see cref="OpCodeId"/>.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static OpDescriptor FromId(OpCode code)
        {
            return FromIdImpl(code);
        }

        /// <summary>
        /// Represents an unknown opcode. Potentially trailing procedure data.
        /// </summary>
        public static OpDescriptor Invalid { get; } = new OpDescriptor(OpCode.Invalid);

        readonly OpCode code;
        readonly OpDescriptorArgList args;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        internal OpDescriptor(OpCode code, params OpDescriptorArg[] args)
        {
            this.code = code;
            this.args = new OpDescriptorArgList(args);
        }

        /// <summary>
        /// Gets the code of the opcode.
        /// </summary>
        public OpCode Code => code;

        /// <summary>
        /// Gets the argument definitions required for this opcode.
        /// </summary>
        public OpDescriptorArgList Args => args;

        /// <summary>
        /// Gets the size of the code within an instruction stream.
        /// </summary>
        public int IdSize => CalculateIdSize();

        /// <summary>
        /// Calculates the size of the code within an instruction stream.
        /// </summary>
        int CalculateIdSize()
        {
            return code == OpCode.Invalid || ((int)code < 0xFB00) ? 1 : 2;
        }

        /// <summary>
        /// Gets the size of the full opcode including arguments within an instruction stream.
        /// </summary>
        public int Size => CalculateSize();

        /// <summary>
        /// Calculates the size of the full opcode including arguments within an instruction stream.
        /// </summary>
        int CalculateSize()
        {
            return IdSize + ArgSize;
        }

        /// <summary>
        /// Gets the size of the opcode arguments within an instruction stream.
        /// </summary>
        public int ArgSize => CalculateArgSize();

        /// <summary>
        /// Calculates the size of the opcode within an instruction stream.
        /// </summary>
        /// <returns></returns>
        int CalculateArgSize()
        {
            return args.Sum(i => i.Size);
        }

        /// <summary>
        /// Returns <c>true</c> if this <see cref="OpDescriptor"/> is equal to the other <see cref="OpDescriptor"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(OpDescriptor other)
        {
            return code == other.code;
        }

        /// <summary>
        /// Returns <c>true</c> if this <see cref="OpDescriptor"/> is equal to the other <see cref="object"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj) && Equals((OpDescriptor)obj);
        }

        /// <summary>
        /// Gets a unique representation of this <see cref="OpDescriptor"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return code.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this opcode.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Code} ({Args})";
        }

    }

}
