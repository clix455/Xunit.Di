using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Xunit.Di
{
    public class DiXunitTestClassRunner : XunitTestClassRunner
    {
        private readonly IServiceScope _serviceScope;

        public DiXunitTestClassRunner(IServiceScope serviceScope,
            ITestClass testClass,
            IReflectionTypeInfo @class,
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource,
            IDictionary<Type, object> collectionFixtureMappings)
            : base(testClass, @class, testCases, diagnosticMessageSink,
                messageBus, testCaseOrderer, aggregator,
                cancellationTokenSource, collectionFixtureMappings) => 
            _serviceScope = serviceScope;

        /// <inheritdoc />
        protected override object[] CreateTestClassConstructorArguments()
        {
            if (this.Class.Type.GetTypeInfo().IsAbstract && this.Class.Type.GetTypeInfo().IsSealed)
                return Array.Empty<object>();

            var constructor = SelectTestClassConstructor();
            if (constructor == null)
                return Array.Empty<object>();

            var parameters = constructor.GetParameters();

            var parameterValues = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; ++i)
            {
                var parameterInfo = parameters[i];
                if (TryGetConstructorArgument(constructor, i, parameterInfo, out var parameterValue))
                    parameterValues[i] = parameterValue;
                else
                {
                    try
                    {
                        parameterValues[i] = _serviceScope.ServiceProvider.GetService(parameterInfo.ParameterType);
                    }
                    catch (Exception exception)
                    {
                        Aggregator.Add(exception);
                    }
                }
            }

            return parameterValues;
        }
    }
}
