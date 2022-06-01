using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using ricaun.Nuke;
using ricaun.Nuke.Components;
using ricaun.Nuke.Extensions;
using System.Linq;

[CheckBuildProjectConfigurations]
class Build : NukeBuild, IPublishRevit, ICompileClient
{
    string IHazRevitPackageBuilder.Application => "Main";
    public static int Main() => Execute<Build>(x => x.From<IPublishRevit>().Build);
}

public interface ICompileClient : ICompile, ISign, IRelease, IHazContent, INukeBuild
{
    /// <summary>
    /// Target CompileClient
    /// </summary>
    Target CompileClient => _ => _
        .TriggeredBy(Compile)
        .Before(Sign)
        .Executes(() =>
        {
            var projects = new[] { "Revit.TestRunner.App", "Revit.TestRunner.Console" };

            foreach (var projectApp in Solution.GetProjects("*").Where(e => projects.Contains(e.Name)))
            {
                Solution.BuildProject(projectApp, (project) =>
                {
                    project.ShowInformation();
                    //SignProject(project);
                    //var contentDirectory = ContentDirectory;
                    //FileSystemTasks.CopyDirectoryRecursively(ContentDirectory, GetBinDirectory(project));
                    //var folder = GetExampleDirectory(project);
                    //var fileName = project.Name;
                    //if (ReleaseExample)
                    //{
                    //    var zipFile = ReleaseDirectory / $"{fileName}.zip";
                    //    ZipExtension.CreateFromDirectory(folder, zipFile);
                    //}
                });
            }
        });

    public AbsolutePath GetBinDirectory(Project project) => project.Directory / "bin";
}