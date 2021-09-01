using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Xunit.Di
{
    public class Setup
    {
        private readonly IList<Action<HostBuilderContext, IConfigurationBuilder>> _configureAppConfigurationActions
            = new List<Action<HostBuilderContext, IConfigurationBuilder>>();
        private readonly IList<Action<HostBuilderContext, IServiceCollection>> _configureServicesActions
            = new List<Action<HostBuilderContext, IServiceCollection>>();
        private readonly IHostBuilder _defaultBuilder;
        private IServiceProvider? _services;
        private bool _built = false;

        public Setup()
        {
            _defaultBuilder = Host.CreateDefaultBuilder();
        }

        protected virtual void Configure()
        {
        }

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application. This can be called multiple times and
        /// the results will be additive. The results will be available at <see cref="HostBuilderContext.Configuration"/> for
        /// subsequent operations, as well as in <see cref="IHost.Services"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _configureAppConfigurationActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return _defaultBuilder;
        }

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        /// to construct the <see cref="IConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            _configureServicesActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return _defaultBuilder;
        }

        public IServiceProvider Services => _services ?? Build();

        private IServiceProvider Build()
        {
            if (_built)
                throw new InvalidOperationException("Build can only be called once.");
            _built = true;
            
            Configure();
            BuildAppConfiguration();
            ConfigureServices();

            _services = _defaultBuilder.Build().Services;
            return _services;
        }

        private void ConfigureServices()
        {
            foreach (var configureServicesAction in _configureServicesActions)
            {
                _defaultBuilder.ConfigureServices(configureServicesAction);
            }
        }

        private void BuildAppConfiguration()
        {
            foreach (var configureAppConfigurationAction in _configureAppConfigurationActions)
            {
                _defaultBuilder.ConfigureAppConfiguration(configureAppConfigurationAction);
            }
        }
    }
}