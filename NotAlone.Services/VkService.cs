using System;
using System.Collections.Generic;
using System.Linq;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace NotAlone.Services
{
    public class VkService : IVkService
    {
        private readonly IVkApi _vkApi;

        public VkService(IVkApi vkApi)
        {
            _vkApi = vkApi;
        }
        
        public void SendMessage(string message, string recipient)
        {
            var user = _vkApi.Users.Get(new List<string> {
                recipient
            }, ProfileFields.All).FirstOrDefault();
            if (user == null)
            {
                throw new Exception($"Пользователь {recipient} не найден");
            }
            _vkApi.Messages.Send(new MessagesSendParams{ 
                RandomId = new DateTime().Millisecond,
                Message = message,
                UserId = user.Id
            });
        }
    }
}