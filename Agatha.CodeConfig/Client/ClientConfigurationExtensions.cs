using Agatha.Common;
using Agatha.Common.WCF;

namespace Agatha.CodeConfig.Client
{
    public static class ClientConfigurationExtensions
    {
        public static ClientConfiguration UseRequestProcessorType<TConfigurator>(this ClientConfiguration clientConfiguration) where TConfigurator : RequestProcessorProxy
        {
            clientConfiguration.RequestProcessorImplementation = typeof(TConfigurator);
            return clientConfiguration;
        }
    }
}
