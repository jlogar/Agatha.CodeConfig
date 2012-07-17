using Agatha.Common.WCF;

namespace Agatha.CodeConfig.Client
{
    /// <summary>
    /// <see cref="RequestProcessorProxy"/> derivate that enables us to configure the WCF endpoint
    /// and binding in code.
    /// </summary>
    public class ConfigurableRequestProcessor : RequestProcessorProxy
    {
        public ConfigurableRequestProcessor(IConfigureProxy configurator)
            : base(configurator.GetBinding(), configurator.GetEndpointAddress())
        { }
    }
}