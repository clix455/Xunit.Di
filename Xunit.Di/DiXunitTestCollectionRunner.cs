using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    public class DiXunitTestCollectionRunner : XunitTestCollectionRunner
    {
        private readonly IServiceScope _serviceScope;

        public DiXunitTestCollectionRunner(
            IServiceProvider provider,
            ITestCollection testCollection,
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            : base(testCollection, testCases, diagnosticMessageSink,
                messageBus, testCaseOrderer, aggregator, cancellationTokenSource)
            => _serviceScope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        /// <inheritdoc/>
        protected override async Task BeforeTestCollectionFinishedAsync()
        {
            _serviceScope.Dispose();
            await base.BeforeTestCollectionFinishedAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<RunSummary> RunTestClassAsync(ITestClass testClass,
            IReflectionTypeInfo @class, IEnumerable<IXunitTestCase> testCases) =>
            new DiXunitTestClassRunner(_serviceScope, testClass, @class, testCases,
                    DiagnosticMessageSink, MessageBus, TestCaseOrderer,
                    new ExceptionAggregator(Aggregator), CancellationTokenSource, CollectionFixtureMappings)
                .RunAsync();
    }
}
