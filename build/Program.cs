using Antda.Build;
using Antda.Build.Extensions;

return BuildHostBuilder
  .CreateDefault()
  .WithProjects("Antda.Core/Antda.Core.csproj")
  .WithSource("src")
  .WithTitle("Antda")
  .WithRepository("Antda", "darlov")
  .UseGithubPackageSource()
  .UseNugetPackageSource()
  .Build()
  .Run(args);