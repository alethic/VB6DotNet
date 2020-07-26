namespace VB6DotNet.PCode
{

    /// <summary>
    /// Indicates that an invalid opcode was encountered in a pcode stream.
    /// </summary>
    public class InvalidOpCodeException : OpCodeException
    {

        readonly OpCode code;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="code"></param>
        public InvalidOpCodeException(OpCode code) :
            this(code, $"Unrecognized opcode {code:X}.")
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public InvalidOpCodeException(OpCode code, string message) :
            base(message)
        {
            this.code = code;
        }

        /// <summary>
        /// Gets the code that was invalid.
        /// </summary>
        public OpCode Code => code;

    }

}
