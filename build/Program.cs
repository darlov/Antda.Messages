using Antda.Build;
using Antda.Build.Extensions;

return BuildHostBuilder
  .CreateDefault()
  .WithProjects("Antda.Core/Antda.Core.csproj", "Antda.Messages/Antda.Messages.csproj", "Antda.Messages.Extensions.Microsoft.DependencyInjection/Antda.Messages.Extensions.Microsoft.DependencyInjection.csproj")
  .WithSource("src")
  .WithTitle("Antda")
  .WithRepository("Antda", "darlov")
  .UseGithubPackageSource()
  .UseNugetPackageSource()
  .Build()
  .Run(args);