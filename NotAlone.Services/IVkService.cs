using System.Text.Json;
using System.Threading.Tasks;

namespace NotAlone.Services
{
    public interface IVkService
    {
        /// <summary>
        /// Send message for recipient from the group with image
        /// </summary>
        /// <param name="message">Message for sending</param>
        /// <param name="recipient">Message recipient</param>
        Task SendMessageWithImage(string message, string recipient, string vkUrl);

        /// <summary>
        /// Method handle any request from vk
        /// </summary>
        /// <param name="request">Event object</param>
        /// <returns>String as status of handling</returns>
        string HandleRequest(JsonElement request);

        /// <summary>
        /// Send message for repicient from the group
        /// </summary>
        /// <param name="message"></param>
        /// <param name="recipient"></param>
        /// <param name="vkUrl"></param>
        /// <returns></returns>
        Task SendMessage(string message, string recipient);
    }
}