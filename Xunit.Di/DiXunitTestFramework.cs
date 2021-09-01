using System;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    public sealed class DiXunitTestFramework: XunitTestFramework
    {
        public DiXunitTestFramework(IMessageSink messageSink) : base(messageSink)
        {
        }

        protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
        {
            IServiceProvider? serviceProvider = default;
            Exception? setupException = default;
            try
            {
                serviceProvider = DiLoader.GetServiceProvider(assemblyName);
            }
            catch (Exception exception)
            {
                setupException = exception;
            }

            var frameworkExecutor = new DiXunitTestFrameworkExecutor(
                serviceProvider, assemblyName, SourceInformationProvider, DiagnosticMessageSink);

            if(setupException != null)
                frameworkExecutor.Aggregator.Add(setupException);
            return frameworkExecutor;
        }
    }
}
