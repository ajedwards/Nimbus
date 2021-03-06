﻿using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Nimbus.Infrastructure;
using Nimbus.Interceptors.Inbound;

namespace Nimbus.Interceptors
{
    public class LargeMessageBodyCleanupInterceptor : InboundInterceptor
    {
        private readonly ILargeMessageBodyStore _largeMessageBodyStore;

        public LargeMessageBodyCleanupInterceptor(ILargeMessageBodyStore largeMessageBodyStore)
        {
            _largeMessageBodyStore = largeMessageBodyStore;
        }

        public override int Priority
        {
            get { return 0; }
        }

        public override async Task OnCommandHandlerSuccess<TBusCommand>(TBusCommand buscommand, BrokeredMessage brokeredMessage)
        {
            await DeleteAssociatedMessageBody(brokeredMessage);
        }

        private async Task DeleteAssociatedMessageBody(BrokeredMessage brokeredMessage)
        {
            object blobIdObject;
            if (!brokeredMessage.Properties.TryGetValue(MessagePropertyKeys.LargeBodyBlobIdentifier, out blobIdObject)) return;

            var blobId = (string) blobIdObject;
            await _largeMessageBodyStore.Delete(blobId);
        }
    }
}