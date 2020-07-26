namespace VB6DotNet.PCode
{

    /// <summary>
    /// Describes the data source of the argument.
    /// </summary>
    public enum OpArgType
    {

        /// <summary>
        /// The argument represents an inline value.
        /// </summary>
        Inline,

        /// <summary>
        /// The argument represents an index within the constant pool.
        /// </summary>
        Constant,

        /// <summary>
        /// The argument represents a reference to a local variable.
        /// </summary>
        Variable

    }

}