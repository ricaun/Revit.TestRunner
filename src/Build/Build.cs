using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

[CheckBuildProjectConfigurations]
class Build : NukeBuild, IPublishRevit
{
    string IHazRevitPackageBuilder.Application => "Main";
    public static int Main() => Execute<Build>(x => x.From<IPublishRevit>().Build);
}