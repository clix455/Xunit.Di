using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace Xunit.Di.Ci.Tests
{
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// Adds an <see cref="FileLoggerProvider"/> to the <see cref="ILoggingBuilder"/>.
        /// </summary>
        /// <param name="builder">The extension method argument.</param>
        /// <returns>The <see cref="ILoggingBuilder"/> so that additional calls can be chained.</returns>
        public static ILoggingBuilder AddFileLog(this ILoggingBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
 
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());
 
            return builder;
        }
    }
}
