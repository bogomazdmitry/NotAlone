using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NotAlone.Models;

namespace NotAlone.Domain.Repositories
{
    public class QueueItemRepository : IQueueItemRepository
    {
        private static string fileName = "queue.json";
        public void Add(QueueItemModel queueItemModel)
        {
            var queue = Get() ?? new List<QueueItemModel>();
            queue?.Add(queueItemModel);
            
            File.WriteAllText(fileName, JsonConvert.SerializeObject(queue));
        }

        public List<QueueItemModel>? Get()
        {
            string queueString;
            lock (fileName)
            {   
                queueString = File.ReadAllText(fileName);
            }
            var queue = JsonConvert.DeserializeObject<List<QueueItemModel>>(queueString);
            return queue;
        }
        
        public void Remove(QueueItemModel queueItemModel)
        {
            var queue = Get();
            var newQueue = 
                queue?.Where(e => e.FirstPerson.VKURL != queueItemModel.FirstPerson.VKURL 
                                  && e.SecondPerson.VKURL != queueItemModel.SecondPerson.VKURL);
            
            lock (fileName)
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(newQueue));
            }
        }

        public void Remove(string firstPersonVkUrl, string secondPersonVkUrl)
        {
            var queue = Get();
            var newQueue = 
                queue?.Where(e => e.FirstPerson.VKURL != firstPersonVkUrl 
                                  && e.SecondPerson.VKURL != secondPersonVkUrl);
            
            lock (fileName)
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(newQueue));
            }
        }

        public bool ExistWithVkUrl(string vkUrl)
        {
            var queue = Get();
            return queue?.Any((e) =>e.FirstPerson.VKURL == vkUrl || e.SecondPerson.VKURL == vkUrl) ?? false;
        }
    }
}