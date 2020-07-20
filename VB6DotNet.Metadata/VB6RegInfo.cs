using System;
using System.Buffers.Binary;
using System.Reflection.PortableExecutable;

using VB6DotNet.Metadata.Extensions;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// If a valid Object needs to be registered, then RegData->bRegInfo will point to the following structure, for each valid Object.
    /// </summary>
    public readonly struct VB6RegInfo
    {

        internal const int Size = 68;

        readonly PEReader pe;
        readonly int offset;
        readonly int regoff;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="pe"></param>
        /// <param name="offset"></param>
        /// <param name="regoff"></param>
        internal unsafe VB6RegInfo(PEReader pe, int offset, int regoff)
        {
            this.pe = pe ?? throw new ArgumentNullException(nameof(pe));
            this.offset = offset;
            this.regoff = regoff;
        }

        public ReadOnlySpan<byte> Span => pe.ToSpan(offset, Size);

        /// <summary>
        /// Reference to COM interfaces info for the next object.
        /// </summary>
        int NextObjectPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x0..0x4]);

        /// <summary>
        /// Object name.
        /// </summary>
        int ObjectNamePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x4..0x8]);

        /// <summary>
        /// Object description.
        /// </summary>
        int ObjectDescriptionPtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x8..0xc]);

        /// <summary>
        /// Instancing mode.
        /// </summary>
        public int Instancing => BinaryPrimitives.ReadInt32LittleEndian(Span[0xc..0x10]);

        /// <summary>
        /// Current object ID in the project.
        /// </summary>
        public int ObjectId => BinaryPrimitives.ReadInt32LittleEndian(Span[0x10..0x14]);

        /// <summary>
        /// CLSID of object.
        /// </summary>
        public Guid ObjectClsId => new Guid(Span[0x14..0x24]);

        /// <summary>
        /// Specifies if the next CLSID is valid.
        /// </summary>
        public int IsInterface => BinaryPrimitives.ReadInt32LittleEndian(Span[0x24..0x28]);

        /// <summary>
        /// Offset to CLSID of object interface.
        /// </summary>
        int UuidObjectIFacePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x28..0x2c]);

        /// <summary>
        /// Offset to CLSID of events interface.
        /// </summary>
        int UuidEventsIFacePtr => BinaryPrimitives.ReadInt32LittleEndian(Span[0x2c..0x30]);

        /// <summary>
        /// Specifies if the CLSID above is valid.
        /// </summary>
        public int HasEvents => BinaryPrimitives.ReadInt32LittleEndian(Span[0x30..0x34]);

        /// <summary>
        /// OLEMISC flags.
        /// </summary>
        public int MiscStatus => BinaryPrimitives.ReadInt32LittleEndian(Span[0x34..0x38]);

        /// <summary>
        /// Class type.
        /// </summary>
        public byte ClassType => Span[0x38..0x39][0];

        /// <summary>
        /// Flags identifying the object type.
        /// </summary>
        public VB6RegInfoObjectTypes ObjectType => (VB6RegInfoObjectTypes)Span[0x39..0x3a][0];

        /// <summary>
        /// Control Bitmap ID in toolbox.
        /// </summary>
        public short ToolboxBitmap32 => BinaryPrimitives.ReadInt16LittleEndian(Span[0x3a..0x3c]);

        /// <summary>
        /// Minimized icon of control window.
        /// </summary>
        public short DefaultIcon => BinaryPrimitives.ReadInt16LittleEndian(Span[0x3c..0x3e]);

        /// <summary>
        /// Specifies whether this is a designer.
        /// </summary>
        public short IsDesigner => BinaryPrimitives.ReadInt16LittleEndian(Span[0x3e..0x40]);

        /// <summary>
        /// Offset to designer data.
        /// </summary>
        int DesignerDataPtr => BinaryPrimitives.ReadInt16LittleEndian(Span[0x40..0x44]);

    }

}

