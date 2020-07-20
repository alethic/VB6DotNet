namespace VB6DotNet.Metadata
{

    public enum VB6ObjectTypeFlags : int
    {

        HasOptionalInfo = 0x1 << 1,
        VB5 = 0x1 << 5,
        IsForm = 0x1 << 7,
        Dll = 0x1 << 11,
        UserControl = 0x1 << 13,
        Unknown2 = 0x1 << 18,
        HasPublicEvents = 0x1 << 19,
        HasPublicInterfaces = 0x1 << 20,

    }

}