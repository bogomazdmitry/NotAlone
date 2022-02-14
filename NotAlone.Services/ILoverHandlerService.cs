using System.Collections.Generic;
using NotAlone.Models;
using System.Threading.Tasks;

namespace NotAlone.Services
{
    public interface ILoverHandlerService
    {
        /// <summary>
        /// Parse string to the LoverPeopleModel
        /// </summary>
        /// <param name="loverPeopleString">String for parsing</param>
        /// <returns>LoverPeopleModel made with string</returns>
        LoverPropertiesModel LoverPeopleFromString(string loverPeopleString);

        /// <summary>
        /// Method create messages for loverPeople and send each other 
        /// </summary>
        /// <param name="firstLoverModel">Info about the first person</param>
        /// <param name="secondLoverModel">Info about the second person</param>
        Task HandlePeople(LoverPropertiesModel firstLoverModel, LoverPropertiesModel secondLoverModel, bool blindDateChecker, string linkBlindDate);
        
        /// <summary>
        /// Method create messages for loverPeople and send each other 
        /// </summary>
        /// <param name="firstLoverPeopleInfo">Info about the first person as string</param>
        /// <param name="secondLoverPeopleInfo">Info about the second person as string</param>
        Task HandlePeople(string firstLoverPeopleInfo, string secondLoverPeopleInfo, bool blindDateChecker, string linkBlindDate);


        Task AddPeopleToQueue(string firstLoverPeopleInfo, string secondLoverPeopleInfo,
            bool blindDateChecker,
            string linkBlindDate);

        Task<List<QueueItemModel>> GetQueueItems();

        Task RemoveQueueItem(QueueItemModel queueItemModel);
        
        Task RemoveQueueItem(string firstPersonVkUrl, string secondPersonVkUrl);
    }
}