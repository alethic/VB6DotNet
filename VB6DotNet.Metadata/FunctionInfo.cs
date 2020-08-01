namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a VB6 function.
    /// </summary>
    public class FunctionInfo : ProcedureInfo
    {

        /// <summary>
        /// Gets or sets the type of the return value.
        /// </summary>
        public TypeReference ReturnType { get; set; }

    }

}
