using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VB6DotNet.Metadata.PortableExecutable.Tests
{

    [TestClass]
    public class VB6MetadataReaderTests
    {

        [TestMethod]
        public void Test_exe()
        {
            var pe = new PEReader(File.OpenRead(@"vb6\TestExe\TestExe.exe"));
            var vb = pe.GetVB6MetadataReader();
            var pi = vb.ProjectInfo;
            pi.Signature.Should().Be("VB5!");
            pi.ProjectTitle.Should().Be("TestExe");
            pi.ProjectName.Should().Be("TestExe");
            pi.ProjectExeName.Should().Be("TestExe");
            pi.HelpFile.Should().Be("TestExeHelpFileName");
            pi.ProjectInfo.ObjectTable.ProjectName.Should().Be("TestExe");
            pi.ProjectInfo.ObjectTable.ProjectInfo2.ProjectDescription.Should().Be("TestExeDesc");
            pi.ProjectInfo.ObjectTable.ProjectInfo2.ProjectHelpFileName.Should().Be("TestExeHelpFileName");

            var frm = pi.ProjectInfo.ObjectTable.Objects.FirstOrDefault(i => i.ObjectName == "TestExeFormA");
            frm.Should().NotBeNull();
            frm.ObjectType.Should().HaveFlag(VB6ObjectTypeFlags.HasOptionalInfo);
            frm.ObjectType.Should().HaveFlag(VB6ObjectTypeFlags.IsForm);
            frm.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.Unknown2);
            frm.ObjectInfo.ProcedureCount.Should().Be(3);
            frm.ObjectInfo.Procedures.Count.Should().Be(3);
            frm.ObjectInfo.Procedures.Should().HaveCount(3);
            frm.ProcedureNames.Should().HaveCount(3);
            frm.ProcedureNames.Should().Contain("FormAMethodA");
            frm.ProcedureNames.Should().Contain("FormAMethodB");
            frm.ProcedureNames.Should().Contain("FormAMethodC");

            var bas = pi.ProjectInfo.ObjectTable.Objects.FirstOrDefault(i => i.ObjectName == "TestExeModuleA");
            bas.Should().NotBeNull();
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.HasOptionalInfo);
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.IsForm);
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.Unknown2);
            bas.ObjectInfo.ProcedureCount.Should().Be(3);
            bas.ObjectInfo.Procedures.Count.Should().Be(3);
            bas.ObjectInfo.Procedures.Should().HaveCount(3);

            var cls = pi.ProjectInfo.ObjectTable.Objects.FirstOrDefault(i => i.ObjectName == "TestExeClassA");
            cls.Should().NotBeNull();
            cls.ObjectType.Should().HaveFlag(VB6ObjectTypeFlags.HasOptionalInfo);
            cls.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.IsForm);
            cls.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.Unknown2);
            cls.ObjectInfo.ProcedureCount.Should().Be(3);
            cls.ObjectInfo.Procedures.Count.Should().Be(3);
            cls.ObjectInfo.Procedures.Should().HaveCount(3);
            cls.ProcedureNames.Should().HaveCount(3);
            cls.ProcedureNames.Should().Contain("ClassAMethodA");
            cls.ProcedureNames.Should().Contain("ClassAMethodB");
            cls.ProcedureNames.Should().Contain("ClassAMethodC");

            var d = cls.ObjectInfo.Procedures[0].ProcCode;
        }

        [TestMethod]
        public void Test_dll()
        {
            var pe = new PEReader(File.OpenRead(@"vb6\TestDll\TestDll.dll"));
            var vb = pe.GetVB6MetadataReader();
            var pi = vb.ProjectInfo;
            pi.Signature.Should().Be("VB5!");
            pi.ProjectTitle.Should().Be("TestDll");
            pi.ProjectName.Should().Be("TestDll");
            pi.ProjectExeName.Should().Be("TestDll");
            pi.HelpFile.Should().Be("TestDllHelpFileName");
            pi.ProjectInfo.ObjectTable.ProjectName.Should().Be("TestDll");
            pi.ProjectInfo.ObjectTable.ProjectInfo2.ProjectDescription.Should().Be("TestDllDesc");
            pi.ProjectInfo.ObjectTable.ProjectInfo2.ProjectHelpFileName.Should().Be("TestDllHelpFileName");

            var bas = pi.ProjectInfo.ObjectTable.Objects.FirstOrDefault(i => i.ObjectName == "TestDllModuleA");
            bas.Should().NotBeNull();
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.HasOptionalInfo);
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.IsForm);
            bas.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.Unknown2);
            bas.ObjectInfo.ProcedureCount.Should().Be(3);
            bas.ObjectInfo.Procedures.Count.Should().Be(3);
            bas.ObjectInfo.Procedures.Should().HaveCount(3);

            var cls = pi.ProjectInfo.ObjectTable.Objects.FirstOrDefault(i => i.ObjectName == "TestDllClassA");
            cls.Should().NotBeNull();
            cls.ObjectType.Should().HaveFlag(VB6ObjectTypeFlags.HasOptionalInfo);
            cls.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.IsForm);
            cls.ObjectType.Should().NotHaveFlag(VB6ObjectTypeFlags.Unknown2);
            cls.ObjectInfo.ProcedureCount.Should().Be(3);
            cls.ObjectInfo.Procedures.Count.Should().Be(3);
            cls.ObjectInfo.Procedures.Should().HaveCount(3);
            cls.ProcedureNames.Should().HaveCount(3);
            cls.ProcedureNames.Should().Contain("ClassAMethodA");
            cls.ProcedureNames.Should().Contain("ClassAMethodB");
            cls.ProcedureNames.Should().Contain("ClassAMethodC");
        }

    }

}
