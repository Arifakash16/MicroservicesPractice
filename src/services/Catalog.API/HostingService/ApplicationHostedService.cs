﻿
using Catalog.API.Context;

namespace Catalog.API.HostingService
{
    public class ApplicationHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CatalogDbContextSeed.Seed();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask; 
        }
    }
}
