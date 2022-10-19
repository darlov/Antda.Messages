using Antda.Messages.Benchmarks.Handlers;
using Antda.Messages.Extensions;
using Antda.Messages.Extensions.Microsoft.DependencyInjection;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Benchmarks;

[MemoryDiagnoser]
public class MessageSenderBenchmark
{
  private readonly IMessageSender _messageSender;
  private readonly IServiceProvider _serviceProvider;
  private readonly BaseMessage _message;

  public MessageSenderBenchmark()
  {
    var serviceCollection = new ServiceCollection();
    serviceCollection.AddAntdaMessages(typeof(BaseMessageHandler).Assembly)
      .UseHandleMessages();
    _serviceProvider = serviceCollection.BuildServiceProvider();
    _messageSender = _serviceProvider.GetRequiredService<IMessageSender>();
    _message = new BaseMessage();
  }

  [Benchmark]
  public async Task MessageSender()
  {
    var _ = await _messageSender.SendAsync(_message);
  }

  [Benchmark(Baseline = true)]
  public async Task DirectCall()
  {
    var _ = await _serviceProvider.GetRequiredService<IMessageHandler<BaseMessage, string>>().HandleAsync(_message, CancellationToken.None);
  }
}