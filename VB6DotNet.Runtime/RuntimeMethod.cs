using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Defines a method that can be invoked.
    /// </summary>
    public abstract class RuntimeMethod
    {

        /// <summary>
        /// Invokes the specified method.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Invoke(IReadOnlyList<object> args);

    }

}
