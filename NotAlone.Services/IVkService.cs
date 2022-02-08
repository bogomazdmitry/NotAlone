using NotAlone.Models;

namespace NotAlone.Services
{
    public interface IVkService
    {
        /// <summary>
        /// Send message for recipient from the group
        /// </summary>
        /// <param name="message">Message for sending</param>
        /// <param name="recipient">Message recipient</param>
        void SendMessage(string message, string recipient);

        /// <summary>
        /// Method handle any request from vk
        /// </summary>
        /// <param name="request">Event object</param>
        /// <returns>String as status of handling</returns>
        string HandleRequest(VkApiRequest request);
    }
}