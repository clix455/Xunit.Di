using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    public class DiXunitTestAssemblyRunner : XunitTestAssemblyRunner
    {
        private readonly IServiceProvider? _provider;

        public DiXunitTestAssemblyRunner(IServiceProvider? provider,
            ITestAssembly testAssembly,
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink diagnosticMessageSink,
            IMessageSink executionMessageSink,
            ITestFrameworkExecutionOptions executionOptions,
            ExceptionAggregator exceptions)
            : base(testAssembly, testCases, diagnosticMessageSink,
                executionMessageSink, executionOptions)
        {
            _provider = provider;
            Aggregator.Aggregate(exceptions);
        }

        /// <inheritdoc />
        protected override Task<RunSummary> RunTestCollectionAsync(IMessageBus messageBus,
            ITestCollection testCollection,
            IEnumerable<IXunitTestCase> testCases,
            CancellationTokenSource cancellationTokenSource)
        {
            if (_provider == null)
                return base.RunTestCollectionAsync(messageBus, testCollection, testCases, cancellationTokenSource);
            return new DiXunitTestCollectionRunner(_provider, testCollection,
                    testCases, DiagnosticMessageSink, messageBus, TestCaseOrderer,
                    new ExceptionAggregator(Aggregator), cancellationTokenSource)
                .RunAsync();
        }
    }
}
