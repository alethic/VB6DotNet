using System;

namespace VB6DotNet.PCode
{

    public class OpCodeException : Exception
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public OpCodeException()
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="message"></param>
        public OpCodeException(string message) :
            base(message)
        {

        }

    }

}