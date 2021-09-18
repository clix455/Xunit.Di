using System;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    /// <summary>
    /// The implementation of <see cref="T:Xunit.Abstractions.ITestFramework" /> that supports
    /// executing xunit tests with dependency injection.
    /// </summary>
    public sealed class DiXunitTestFramework : XunitTestFramework
    {
        /// <inheritdoc/>
        public DiXunitTestFramework(IMessageSink messageSink) : base(messageSink)
        {
        }

        internal ExceptionAggregator Aggregator { get; set; } = new ExceptionAggregator();
        
        /// <inheritdoc/>
        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        {
            try
            {
                var serviceProvider = DiLoader.GetServiceProvider(assemblyName);
                if (serviceProvider != null)
                    return new DiXunitTestFrameworkExecutor(
                        serviceProvider, assemblyName, SourceInformationProvider, DiagnosticMessageSink);
            }
            catch (Exception exception)
            {
                Aggregator.Add(exception);
            }

            return base.CreateExecutor(assemblyName);
        }
    }
}
