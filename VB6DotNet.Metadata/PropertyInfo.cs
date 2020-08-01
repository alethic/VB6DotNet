namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a property on a <see cref="ObjectInfo"/>.
    /// </summary>
    public class PropertyInfo : MemberInfo
    {

        /// <summary>
        /// Describes the type of the property.
        /// </summary>
        public TypeReference PropertyType { get; set; }

        /// <summary>
        /// Describes the getter of the property.
        /// </summary>
        public FunctionInfo Getter { get; set; }

        /// <summary>
        /// Describes the setter of the property.
        /// </summary>
        public SubInfo Setter { get; set; }

    }

}
