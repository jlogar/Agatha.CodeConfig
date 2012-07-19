using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Agatha.CodeConfig.Client
{
    /// <summary>
    /// Provides the bindings and endpoint address for <see cref="ConfigurableRequestProcessor"/>.
    /// </summary>
    public class DefaultRequestProcessorConfig : IConfigureProxy
    {
        public virtual Binding GetBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            return binding;
        }

        public virtual EndpointAddress GetEndpointAddress()
        {
            return new EndpointAddress("http://localhost:10000/");
        }
    }
}