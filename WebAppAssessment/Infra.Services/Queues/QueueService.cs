using System.Threading.Tasks;
using Azure.Storage.Queues;
using Domain.Model.Interfaces.Infrastructure;

namespace Infra.Services.Queues
{
    public class QueueService : IQueueService
    {
        private readonly QueueServiceClient _queueServiceClient;
        private const string _queueName = "quantity-view";

        public QueueService(string storageAccount)
        {
            _queueServiceClient = new QueueServiceClient(storageAccount);
        }

        public async Task SendAsync(string messageText)
        {
            var queueClient = _queueServiceClient.GetQueueClient(_queueName);

            await queueClient.CreateIfNotExistsAsync();

            await queueClient.SendMessageAsync(messageText);
        }
    }
}
