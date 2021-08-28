using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    public sealed class DiXunitTestFrameworkExecutor : XunitTestFrameworkExecutor
    {
        private readonly IServiceProvider? _serviceProvider;

        public DiXunitTestFrameworkExecutor(IServiceProvider? serviceProvider, AssemblyName assemblyName,
            ISourceInformationProvider sourceInformationProvider, IMessageSink diagnosticMessageSink)
            : base(assemblyName, sourceInformationProvider, diagnosticMessageSink) =>
            _serviceProvider = serviceProvider;

        internal ExceptionAggregator Aggregator { get; set; } = new ExceptionAggregator();

        /// <inheritdoc />
        protected override async void RunTestCases(
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink executionMessageSink,
            ITestFrameworkExecutionOptions executionOptions)
        {
            await new DiXunitTestAssemblyRunner(_serviceProvider, TestAssembly, testCases, DiagnosticMessageSink,
                executionMessageSink, executionOptions, Aggregator).RunAsync();
        }
    }
}
