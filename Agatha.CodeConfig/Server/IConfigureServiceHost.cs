using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Agatha.CodeConfig.Server
{
    public interface IConfigureServiceHost
    {
        Uri GetBaseAddress();
        IEnumerable<IServiceBehavior> GetBehaviors();
        Binding GetBinding();
    }
}