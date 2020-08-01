namespace System
{

    public static class GuidExtensions
    {

        public static Guid ToGuid(ReadOnlySpan<byte> bytes)
        {
#if NETSTANDARD2_0
            return new Guid(bytes.ToArray());
#else
            return new Guid(bytes);
#endif
        }

    }

}
