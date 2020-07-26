namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a property on a <see cref="Object"/>.
    /// </summary>
    public class Property : Member
    {

        /// <summary>
        /// Describes the type of the property.
        /// </summary>
        public TypeReference PropertyType { get; set; }

        /// <summary>
        /// Describes the getter of the property.
        /// </summary>
        public Function Getter { get; set; }

        /// <summary>
        /// Describes the setter of the property.
        /// </summary>
        public Sub Setter { get; set; }

    }

}
