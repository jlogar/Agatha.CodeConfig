using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Agatha.CodeConfig.Server
{
    public class DefaultServiceHostConfig : IConfigureServiceHost
    {
        public virtual Uri GetBaseAddress()
        {
            return new Uri("http://localhost:10000/");
        }

        public virtual IEnumerable<IServiceBehavior> GetBehaviors()
        {
            var serviceMetadataBehavior = new ServiceMetadataBehavior
                                              {
                                                  HttpGetEnabled = true,
                                                  MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                                              };
            var serviceDebugBehavior = new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true };
            return new IServiceBehavior[] { serviceMetadataBehavior, serviceDebugBehavior };
        }

        public virtual Binding GetBinding()
        {
            return new BasicHttpBinding(BasicHttpSecurityMode.None);
        }
    }
}