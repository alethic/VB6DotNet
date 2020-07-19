using System.IO;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VB6DotNet.Parser.Tests
{

    [TestClass]
    public class VisualBasic6ParserTests
    {

        [TestMethod]
        public void Test_example1()
        {
            var p = new VisualBasic6Parser();
            p.Parse(File.OpenRead(@"examples\example1.bas_"));
            p.Modules.Should().HaveCount(1);
        }

    }

}
