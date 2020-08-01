#if NETSTANDARD2_0

namespace System.Text
{

    public static class EncodingExtensions
    {

        public static string GetString(this Encoding encoding, ReadOnlySpan<byte> bytes)
        {
            return encoding.GetString(bytes.ToArray());
        }

    }

}

#endif
