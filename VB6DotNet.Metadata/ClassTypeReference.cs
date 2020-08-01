﻿namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a reference to a <see cref="ClassModule"/>.
    /// </summary>
    public class ClassTypeReference : TypeReference
    {

        /// <summary>
        /// Gets or sets the class module being referenced.
        /// </summary>
        public ClassInfo ClassModule { get; set; }

    }

}