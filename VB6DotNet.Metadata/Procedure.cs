using System.Collections.Generic;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes an invokable procedure.
    /// </summary>
    public abstract class Procedure : Member
    {

        /// <summary>
        /// Gets the set of parameters that can be passed to the procedure.
        /// </summary>
        public IList<Parameter> Parameters { get; } = new List<Parameter>();

    }

}
