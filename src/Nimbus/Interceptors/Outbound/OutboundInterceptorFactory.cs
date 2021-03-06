using System.Linq;
using Nimbus.Configuration.Settings;
using Nimbus.DependencyResolution;

namespace Nimbus.Interceptors.Outbound
{
    internal class OutboundInterceptorFactory : IOutboundInterceptorFactory
    {
        private readonly GlobalOutboundInterceptorTypesSetting _globalOutboundInterceptorTypes;

        public OutboundInterceptorFactory(GlobalOutboundInterceptorTypesSetting globalOutboundInterceptorTypes)
        {
            _globalOutboundInterceptorTypes = globalOutboundInterceptorTypes;
        }

        public IOutboundInterceptor[] CreateInterceptors(IDependencyResolverScope scope)
        {
            return _globalOutboundInterceptorTypes
                .Value
                .Select(t => (IOutboundInterceptor) scope.Resolve(t))
                .ToArray();
        }
    }
}