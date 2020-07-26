using System.Collections.Generic;
using System.Reflection.PortableExecutable;

using VB6DotNet.PortableExecutable;

namespace VB6DotNet.Metadata
{

    /// <summary>
    /// Describes a VB6 project.
    /// </summary>
    public class Project
    {

        /// <summary>
        /// Loads a project from the given VB6 metadata.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Project Load(PEReader reader)
        {
            return Load(reader.GetVB6MetadataReader());
        }

        /// <summary>
        /// Loads a project from the given VB6 metadata.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Project Load(VB6MetadataReader reader)
        {
            var p = new Project();
            if (reader.PortableExecutable.PEHeaders.IsExe)
                p.Type = ProjectType.StandardExe;
            if (reader.PortableExecutable.PEHeaders.IsDll)
                p.Type = ProjectType.ActiveXDll;
            p.Name = reader.ProjectInfo.ProjectName;
            p.HelpFileName = reader.ProjectInfo.HelpFile;
            p.Description = reader.ProjectInfo.ProjectInfo.ObjectTable.ProjectInfo2.ProjectDescription;
            if (reader.ProjectInfo.ThreadFlags.HasFlag(VB6ExeProjectInfoThreadFlags.SingleThreaded))
                p.ThreadingModel = ThreadingModel.Single;
            if (reader.ProjectInfo.ThreadFlags.HasFlag(VB6ExeProjectInfoThreadFlags.ApartmentModel))
                p.ThreadingModel = ThreadingModel.Apartment;

            foreach (var o in reader.ProjectInfo.ProjectInfo.ObjectTable.Objects)
                if (o.ObjectType.HasFlag(VB6ObjectTypeFlags.HasOptionalInfo) &&
                    o.ObjectType.HasFlag(VB6ObjectTypeFlags.IsForm))
                    p.Objects.Add(Form.Load(o));

            foreach (var o in reader.ProjectInfo.ProjectInfo.ObjectTable.Objects)
                if (o.ObjectType.HasFlag(VB6ObjectTypeFlags.HasOptionalInfo) == false &&
                    o.ObjectType.HasFlag(VB6ObjectTypeFlags.IsForm) == false)
                    p.Objects.Add(Module.Load(o));

            foreach (var o in reader.ProjectInfo.ProjectInfo.ObjectTable.Objects)
                if (o.ObjectType.HasFlag(VB6ObjectTypeFlags.HasOptionalInfo) &&
                    o.ObjectType.HasFlag(VB6ObjectTypeFlags.IsForm) == false)
                    p.Objects.Add(ClassModule.Load(o));

            return p;
        }

        /// <summary>
        /// Gets or sets the project type.
        /// </summary>
        public ProjectType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the help file.
        /// </summary>
        public string HelpFileName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the startup object.
        /// </summary>
        public Object StartupObject { get; set; }

        /// <summary>
        /// Gets or sets the threading model.
        /// </summary>
        public ThreadingModel ThreadingModel { get; set; }

        /// <summary>
        /// Gets all of the objects within the project.
        /// </summary>
        public ICollection<Object> Objects { get; set; } = new List<Object>();

    }

}
