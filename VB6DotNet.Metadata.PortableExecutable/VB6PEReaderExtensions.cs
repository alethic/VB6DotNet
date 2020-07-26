using System.Reflection.PortableExecutable;

namespace VB6DotNet.Metadata.PortableExecutable
{

    /// <summary>
    /// Provides extensions to the <see cref="PEReader"/> class.
    /// </summary>
    public static class VB6PEReaderExtensions
    {

        /// <summary>
        /// Gets a <see cref="VB6MetadataReader"/> for the specified <see cref="PEReader"/>.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static VB6MetadataReader GetVB6MetadataReader(this PEReader self)
        {
            return new VB6MetadataReader(self);
        }

    }

}
