namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a member of an object.
    /// </summary>
    public abstract class Member
    {

        /// <summary>
        /// Gets the object that owns the member.
        /// </summary>
        public Object Object { get; set; }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        public string Name { get; set; }

    }

}