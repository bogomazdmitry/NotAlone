using System.Collections.Generic;
using NotAlone.Models;

namespace NotAlone.Domain.Repositories
{
    public interface IQueueItemRepository
    {
        void Add(QueueItemModel queueItemModel);

        List<QueueItemModel> Get();

        void Remove(QueueItemModel queueItemModel);

        void Remove(string firstPersonVkUrl, string secondPersonVkUrl);

        bool ExistWithVkUrl(string vkUrl);
    }
}