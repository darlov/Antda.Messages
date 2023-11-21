using Antda.Build;
using Antda.Build.Extensions;

return BuildHostBuilder
  .CreateDefault()
  .WithProjects(
    "Antda.Messages.Core/Antda.Messages.Core.csproj",
    "Antda.Messages/Antda.Messages.csproj", 
    "Antda.Messages.Extensions.Microsoft.DependencyInjection/Antda.Messages.Extensions.Microsoft.DependencyInjection.csproj"
)
  .WithSource("src")
  .WithTitle("Antda.Messages")
  .WithRepository("Antda.Messages", "darlov")
  .UseNugetPackageSource()
  .Build()
  .Run(args);