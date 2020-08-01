using System;

using VB6DotNet.Runtime.Threading;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Represents an object during runtime.
    /// </summary>
    public abstract class RuntimeObject
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="apartment"></param>
        public RuntimeObject(RuntimeObjectType type, Apartment apartment)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Apartment = apartment ?? throw new ArgumentNullException(nameof(apartment));
        }

        /// <summary>
        /// Gets the type that declares the object instance.
        /// </summary>
        public RuntimeObjectType Type { get; }

        /// <summary>
        /// Gets the apartment that owns the object instance.
        /// </summary>
        public Apartment Apartment { get; }

    }

}
