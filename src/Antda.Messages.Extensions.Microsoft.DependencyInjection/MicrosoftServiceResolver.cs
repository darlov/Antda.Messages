﻿using Microsoft.Extensions.DependencyInjection;

namespace Antda.Messages.Extensions.Microsoft.DependencyInjection;

public class MicrosoftServiceResolver : IServiceResolver
{
    private readonly IServiceProvider _serviceProvider;

    public MicrosoftServiceResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public object GetRequiredService(Type serviceType) => _serviceProvider.GetRequiredService(serviceType);
}