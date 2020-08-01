using System;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes a type at runtime.
    /// </summary>
    public abstract class RuntimeType
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="name"></param>
        public RuntimeType(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Gets or sets the name of the interface.
        /// </summary>
        public string Name { get; }

    }

}