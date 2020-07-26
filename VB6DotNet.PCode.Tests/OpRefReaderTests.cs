using System.Collections.Generic;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VB6DotNet.PCode.Tests
{

    [TestClass]
    public class OpRefReaderTests
    {

        [TestMethod]
        public void Should_read_integer_to_variant_assignment_module_sub()
        {
            var dat = new byte[]
            {
                0x28, 0x5C, 0xFF, 0x01, 0x00,   // LitVarI2 ([Variant] var_A4, [Integer] 0100)
                0xFC, 0xF6, 0x6C, 0xFF,         // FStVar ([Variant] var_94)
                0x14                            // ExitProc ()
            };

            var rdr = new OpRefReader(dat);
            var ops = new List<Op>();
            while (rdr.Read(out var op))
                ops.Add(op.ToOp());

            ops.Should().HaveCount(3);

            ops[0].Descriptor.Code.Should().Be(OpCode.LitVarI2);
            ops[0].Args.Should().HaveCount(2);
            ops[0].Args[0].Descriptor.Type.Should().Be(OpArgType.Variable);
            ops[0].Args[0].Descriptor.ValueType.Should().Be(OpArgValueType.Variant);
            ops[0].Args[1].Descriptor.Type.Should().Be(OpArgType.Inline);
            ops[0].Args[1].Descriptor.ValueType.Should().Be(OpArgValueType.Integer);

            ops[1].Descriptor.Code.Should().Be(OpCode.FStVar);
            ops[1].Args.Should().HaveCount(1);
            ops[1].Args[0].Descriptor.Type.Should().Be(OpArgType.Variable);
            ops[1].Args[0].Descriptor.ValueType.Should().Be(OpArgValueType.Variant);

            ops[2].Descriptor.Code.Should().Be(OpCode.ExitProc);
            ops[2].Args.Should().HaveCount(0);
        }

    }

}
