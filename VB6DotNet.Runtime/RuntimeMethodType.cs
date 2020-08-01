using System;
using System.Collections.Generic;

namespace VB6DotNet.Runtime
{

    /// <summary>
    /// Describes a method available on an interface type.
    /// </summary>
    public class RuntimeMethodType
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="returnType"></param>
        /// <param name="parameters"></param>
        public RuntimeMethodType(RuntimeType returnType, IReadOnlyList<RuntimeType> parameters)
        {
            ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
        }

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public RuntimeType ReturnType { get; }

        /// <summary>
        /// Gets the parameters of the method.
        /// </summary>
        public IReadOnlyList<RuntimeType> Parameters { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public object Invoke(RuntimeInvocation invocation)
        {

        }

    }

}