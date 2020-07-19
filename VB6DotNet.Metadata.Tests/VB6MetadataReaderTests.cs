using System.IO;
using System.Reflection.PortableExecutable;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VB6DotNet.Metadata.Tests
{

    [TestClass]
    public class VB6MetadataReaderTests
    {

        [TestMethod]
        public void Test_exe()
        {
            var pe = new PEReader(File.OpenRead(@"C:\dev\vb6exm\TestExe.exe"));
            var vb = pe.GetVB6MetadataReader();
            var pi = vb.ProjectInfo;
            pi.Magic.Should().Be("VB5!");
            pi.ProjectExeName.Should().Be("TestExe");
            pi.ProjectName.Should().Be("TestExe");
            pi.ProjectDescription.Should().Be("TestExe");
            pi.ProjectHelpFile.Should().Be("TestExeHelpFileName");
            pi.ProjectData.ObjectTable.ProjectName.Should().Be("TestExe");
            pi.ProjectData.ObjectTable.ProjectInfo2.ProjectDescription.Should().Be("TestExeDesc");
        }

        [TestMethod]
        public void Test_dll()
        {
            var pe = new PEReader(File.OpenRead(@"C:\dev\vb6exm\TestDll.dll"));
            var vb = pe.GetVB6MetadataReader();
            var pi = vb.ProjectInfo;
            var mg = pi.Magic;
            var nm = pi.ProjectExeName;
            var zn = pi.ProjectName;
            var vz = pi.ProjectData.Version;
            var ob = pi.ProjectData.ObjectTable;
            var zz = pi.ProjectData.ObjectTable.ProjectName;
        }

    }

}
