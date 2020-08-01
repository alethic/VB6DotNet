using VB6DotNet.Runtime.Threading;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Represents an instance of a VB6 class module type.
    /// </summary>
    class RuntimeClassObject : RuntimeObject
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="apartment"></param>
        public RuntimeClassObject(RuntimeClassObjectType type, Apartment apartment) :
            base(type, apartment)
        {

        }

        /// <summary>
        /// Gets the type that declares the object instance.
        /// </summary>
        public new RuntimeClassObjectType Type => (RuntimeClassObjectType)base.Type;

    }

}
