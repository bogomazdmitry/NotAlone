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
        /// <returns>Message for LoverPeople</returns>
        string CreateMessage(LoverPropertiesModel fromLover, LoverPropertiesModel toLover);

        /// <summary>
        /// Method create message for autosanding for blind dates
        /// </summary>
        /// <param name="fromLover"></param>
        /// <param name="toLover"></param>
        /// <returns>Message for LoverPeople who pick blind date</returns>
        string CreateMessageForBlindDate(LoverPropertiesModel fromLover, LoverPropertiesModel toLover, string linkBlindDate);

        /// <summary>
        /// Method return comment string for creating message
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Comment</returns>
        string ReturnComment(LoverPropertiesModel model);

        /// <summary>
        /// Method return faculty string for creating message
        /// </summary>
        /// <param name="model"></param>
        /// <param name="secondPartnerSexPropertries"></param>
        /// <returns>Faculty</returns>
        string ReturnFaculty(LoverPropertiesModel model, string secondPartnerSexPropertries);

        /// <summary>
        /// Method return event aim string for creatin message
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Event aim</returns>
        string ReturnEventAim(LoverPropertiesModel model);
        
    }
}