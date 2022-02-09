using NotAlone.Models;

namespace NotAlone.Services
{
    public interface ICreateMessageService
    {
        /// <summary>
        /// Method create message for autosending
        /// </summary>
        /// <param name="fromLover">The person whose profile is used to create the message</param>
        /// <param name="toLover">The person to whom the message is intended</param>
        /// <returns>Message for toLoverPeople</returns>
        string CreateMessage(LoverModel fromLover, LoverModel toLover);
    }
}