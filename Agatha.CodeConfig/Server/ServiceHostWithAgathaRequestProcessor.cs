using System;
using System.Linq;
using System.ServiceModel;
using Agatha.Common.WCF;
using Common.Logging;

namespace Agatha.CodeConfig.Server
{
    /// <summary>
    /// Provides Agatha service hosting in code so that your config files don't get cluttered with WCF config.
    /// </summary>
    public class ServiceHostWithAgathaRequestProcessor
    {
        private readonly IConfigureServiceHost _configureServiceHost;
        private readonly ILog _log = LogManager.GetLogger(typeof(ServiceHostWithAgathaRequestProcessor));
        private ServiceHost _host;

        public ServiceHostWithAgathaRequestProcessor(IConfigureServiceHost configureServiceHost)
        {
            _configureServiceHost = configureServiceHost;
        }

        /// <summary>
        /// Starts the WCF service host and registers Agatha Request processor.
        /// </summary>
        /// <returns>False on failure to set up the WCF endpoint and WCF servicehost opening</returns>
        public bool Start()
        {
            _log.Info("Creating WCF host.");
            CreateServiceHostAndBinding();

            _log.Info("Starting WCF host.");
            try
            {
                _host.Open();
            }
            catch (Exception e)
            {
                HandleExceptionAndDisposeHost(e);
                return false;
            }

            _log.Info(string.Format("Host service is ready at {0}", _configureServiceHost.GetBaseAddress()));
            return true;
        }

        private void HandleExceptionAndDisposeHost(Exception e)
        {
            //logging for testing purposes (usualy there is no logger defined when running tests
            //and having errors not logged makes it hard to see why a test failed)
            Console.WriteLine(string.Format("Error opening WCF host at address {0}.", _configureServiceHost.GetBaseAddress()));
            Console.WriteLine(string.Format("{0}\n{1}", e.Message, e.StackTrace));
            _log.Fatal(string.Format("Error opening WCF host at address {0}.", _configureServiceHost.GetBaseAddress()), e);

            _log.Info("Disposing WCF host.");
            if (_host.State == CommunicationState.Faulted)
                _host.Abort();
            IDisposable d = _host;
            d.Dispose();
        }

        private void CreateServiceHostAndBinding()
        {
            _host = new ServiceHost(typeof(ServiceLayer.WCF.WcfRequestProcessor), _configureServiceHost.GetBaseAddress());
            var behaviors = _configureServiceHost.GetBehaviors().ToArray();
            foreach (var serviceBehavior in behaviors)
            {
                if (_host.Description.Behaviors.Contains(serviceBehavior.GetType()))
                    _host.Description.Behaviors.Remove(serviceBehavior.GetType());
                _host.Description.Behaviors.Add(serviceBehavior);
            }
            var binding = _configureServiceHost.GetBinding();
            _host.AddServiceEndpoint(typeof(IWcfRequestProcessor), binding, _configureServiceHost.GetBaseAddress());
        }

        public void Stop()
        {
            _log.Info("Host service is stopping.");
            if (_host.State == CommunicationState.Faulted)
                _host.Abort();
            else
                _host.Close();
            IDisposable d = _host;
            d.Dispose();
            _log.Info("Host service stoped and disposed.");
        }
    }
}