using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VB6DotNet.PCode.Tests
{

    [TestClass]
    public class Class1
    {

        public static void Foo()
        {
            var re = new PCodeReader(new System.Buffers.ReadOnlySequence<byte>());
            re.Read();
        }

    }

}
