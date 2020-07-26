using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes an instruction within a P-Code stream.
    /// </summary>
    public readonly ref struct OpRef
    {

        readonly ReadOnlySpan<byte> buffer;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer"></param>
        public OpRef(ReadOnlySpan<byte> buffer)
        {
            this.buffer = buffer;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer"></param>
        public OpRef(ReadOnlyMemory<byte> buffer) :
            this(buffer.Span)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer"></param>
        public OpRef(byte[] buffer) :
            this(new ReadOnlySpan<byte>(buffer))
        {

        }

        /// <summary>
        /// Gets the code of the instruction.
        /// </summary>
        public OpDescriptor Code => OpDescriptor.FromId(buffer[0] < 0xFB ? (OpCode)buffer[0] : (OpCode)(buffer[0] << 8 | buffer[1]));

        /// <summary>
        /// Gets the set of arguments of the operation.
        /// </summary>
        public OpRefArgList Args => new OpRefArgList(Code.Args, buffer.Slice(Code.IdSize, Code.ArgSize));

        /// <summary>
        /// Converts the <see cref="OpRef"/> to a <see cref="Op"/>.
        /// </summary>
        /// <returns></returns>
        public Op ToOp()
        {
            var l = new OpArgList(Code.Args);
            foreach (var a in Args)
                l.Add(a.ToOpArg());

            return new Op(Code, l);
        }

        /// <summary>
        /// Returns a string representation of this operation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Code.Code} ({Args.ToString()})";
        }

    }

}
