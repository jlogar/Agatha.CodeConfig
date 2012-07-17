using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Agatha.CodeConfig
{
    public interface IConfigureProxy
    {
        Binding GetBinding();
        EndpointAddress GetEndpointAddress();
    }
}