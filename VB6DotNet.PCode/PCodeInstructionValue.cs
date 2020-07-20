
using VBDotNet6.PCode;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Provides a representation of a PCode instruction.
    /// </summary>
    public readonly struct PCodeInstructionValue
    {

        readonly PCodeOp op;
        readonly PCodeArg arg1;
        readonly PCodeArg arg2;
        readonly PCodeArg arg3;
        readonly PCodeArg arg4;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="op"></param>
        internal PCodeInstructionValue(PCodeOp op)
        {
            this.op = op;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="arg1"></param>
        internal PCodeInstructionValue(PCodeOp op, PCodeArg arg1)
        {
            this.op = op;
            this.arg1 = arg1;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        internal PCodeInstructionValue(PCodeOp op, PCodeArg arg1, PCodeArg arg2)
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        internal PCodeInstructionValue(PCodeOp op, PCodeArg arg1, PCodeArg arg2, PCodeArg arg3)
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        /// <param name="arg4"></param>
        internal PCodeInstructionValue(PCodeOp op, PCodeArg arg1, PCodeArg arg2, PCodeArg arg3, PCodeArg arg4)
        {
            this.op = op;
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.arg3 = arg3;
            this.arg4 = arg4;
        }

    }

}
