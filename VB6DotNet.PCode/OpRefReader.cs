using System;

namespace VB6DotNet.PCode
{

    /// <summary>
    /// Provides access through a op stream.
    /// </summary>
    public ref struct OpRefReader
    {

        readonly ReadOnlySpan<byte> buffer;
        int position;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="position"></param>
        public OpRefReader(ReadOnlySpan<byte> buffer, int position = 0)
        {
            this.buffer = buffer;
            this.position = position;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="position"></param>
        public OpRefReader(byte[] buffer, int position = 0) :
            this(new ReadOnlySpan<byte>(buffer), position)
        {

        }

        /// <summary>
        /// Reads the next operation from the stream.
        /// </summary>
        /// <returns></returns>
        public bool Read(out OpRef op)
        {
            // exit if we've reached the end
            op = new OpRef();
            if (position >= buffer.Length)
                return false;

            // find opcode from first one or two bytes
            var c = OpDescriptor.FromId(buffer[position] < 0xFB ? (OpCode)buffer[position] : (OpCode)(buffer[position] << 8 | buffer[position + 1]));

            // slice op
            var l = c.Size;
            var s = buffer.Slice(position, l);

            // next position is end of current
            position += l;

            // return op
            op = new OpRef(s);
            return true;
        }

    }

}
