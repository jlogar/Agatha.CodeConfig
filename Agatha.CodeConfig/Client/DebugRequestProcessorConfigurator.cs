using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Agatha.CodeConfig
{
    /// <summary>
    /// Provides the bindings and endpoint addresses for <see cref="ConfigurableRequestProcessor"/>.
    /// </summary>
    public class DebugRequestProcessorConfigurator : IConfigureProxy
    {
        public Binding GetBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            return binding;
        }

        public EndpointAddress GetEndpointAddress()
        {
            return new EndpointAddress("http://localhost:10000/");
        }
    }
}