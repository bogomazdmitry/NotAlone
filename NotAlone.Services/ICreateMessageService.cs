using NotAlone.Models;

namespace NotAlone.Services
{
    public interface ICreateMessageService
    {
        /// <summary>
        /// Method create message for autosending
        /// </summary>
        /// <param name="fromLoverPeople">The person whose profile is used to create the message</param>
        /// <param name="toLoverPeople">The person to whom the message is intended</param>
        /// <returns>Message for toLoverPeople</returns>
        string CreateMessage(LoverPeopleModel fromLoverPeople, LoverPeopleModel toLoverPeople);
    }
}