using Antda.Messages.Benchmarks.Handlers;
using Antda.Messages.Extensions;
using Antda.Messages.Extensions.Microsoft.DependencyInjection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Benchmarks;

[MemoryDiagnoser]
 // [EtwProfiler(performExtraBenchmarksRun: false)]
public class MessageSenderBenchmark
{
  private readonly IServiceProvider _serviceProvider;
  private readonly BaseMessage _message;

  public MessageSenderBenchmark()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddAntdaMessages(typeof(BaseMessageHandler).Assembly)
      .UseHandleMessages();
    serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(MessageSenderBenchmark)));
    _serviceProvider = serviceCollection.BuildServiceProvider();
    _message = new BaseMessage();


  }

  [Benchmark]
  public async Task MediatR()
  {
    var _ = await _serviceProvider.GetRequiredService<IMediator>().Send(_message);
  }
  
  
  [Benchmark]
  public async Task MessageSender()
  {
    var _ = await _serviceProvider.GetRequiredService<IMessageSender>().SendAsync(_message);
  }

  [Benchmark(Baseline = true)]
  public async Task DirectCall()
  {
    var _ = await _serviceProvider.GetRequiredService<IMessageHandler<BaseMessage, string>>().HandleAsync(_message, CancellationToken.None);
  }
  

}