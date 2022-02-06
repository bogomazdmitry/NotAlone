using NotAlone.Models;

namespace NotAlone.Services
{
    public interface ILoverHandlerService
    {
        /// <summary>
        /// Parse string to the LoverPeopleModel
        /// </summary>
        /// <param name="loverPeopleString">String for parsing</param>
        /// <returns>LoverPeopleModel made with string</returns>
        LoverPeopleModel LoverPeopleFromString(string loverPeopleString);

        /// <summary>
        /// Method create messages for loverPeople and send each other 
        /// </summary>
        /// <param name="firstLoverPeopleModel">Info about the first person</param>
        /// <param name="secondLoverPeopleModel">Info about the second person</param>
        void HandlePeople(LoverPeopleModel firstLoverPeopleModel, LoverPeopleModel secondLoverPeopleModel);
        
        /// <summary>
        /// Method create messages for loverPeople and send each other 
        /// </summary>
        /// <param name="firstLoverPeopleInfo">Info about the first person as string</param>
        /// <param name="secondLoverPeopleInfo">Info about the second person as string</param>
        void HandlePeople(string firstLoverPeopleInfo, string secondLoverPeopleInfo);
    }
}