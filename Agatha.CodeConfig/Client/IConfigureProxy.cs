using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Agatha.CodeConfig.Client
{
    public interface IConfigureProxy
    {
        Binding GetBinding();
        EndpointAddress GetEndpointAddress();
    }
}