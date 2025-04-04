using Antda.Messages.Benchmarks.Handlers;
using Antda.Messages.Extensions.Microsoft.DependencyInjection;
using BenchmarkDotNet.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Benchmarks;

[MemoryDiagnoser]
 // [EtwProfiler(performExtraBenchmarksRun: false)]
 [InProcess]
public class MessageSenderBenchmark
{
  private readonly IServiceProvider _serviceProvider;
  private readonly BaseMessage _message;

  public MessageSenderBenchmark()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddAntdaMessages(cfg => cfg.RegisterHandlersFromAssembly<BaseMessageHandler>());
    serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<MessageSenderBenchmark>());
    _serviceProvider = serviceCollection.BuildServiceProvider();
    _message = new BaseMessage();
  }

  [Benchmark]
  public async Task MediatR()
  {
    _ = await _serviceProvider.GetRequiredService<IMediator>().Send(_message);
  }
  
  
  [Benchmark]
  public async Task MessageSender()
  {
    _ = await _serviceProvider.GetRequiredService<IMessageSender>().SendAsync(_message);
  }

  [Benchmark(Baseline = true)]
  public async Task DirectCall()
  {
    _ = await _serviceProvider.GetRequiredService<IMessageHandler<BaseMessage, string>>().HandleAsync(_message, CancellationToken.None);
  }
  

}