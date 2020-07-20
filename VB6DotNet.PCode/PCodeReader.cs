using System.Buffers;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Provides a forward-only reader over a stream of P-Codes.
    /// </summary>
    public class PCodeReader
    {

        readonly ReadOnlySequence<byte> code;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="code"></param>
        public PCodeReader(ReadOnlySequence<byte> code)
        {
            this.code = code;
        }

    }

}
