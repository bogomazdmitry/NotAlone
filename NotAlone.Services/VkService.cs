using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace NotAlone.Services
{
    public class VkService : IVkService
    {
        private readonly IVkApi _vkApi;
        
        private readonly ILogger<VkService> _logger;

        public VkService(IVkApi vkApi, ILogger<VkService> logger)
        {
            _vkApi = vkApi;
            _logger = logger;
        }
        
        public void SendMessage(string message, string recipient)
        {
            var recipientId = recipient
                .Split("/")
                .Last(e => !string.IsNullOrEmpty(e));
            
            var user = _vkApi.Users.Get(new List<string> {
                recipientId
            }, ProfileFields.Uid).FirstOrDefault();
            if (user == null)
            {
                throw new Exception($"Пользователь с id: {recipientId} не найден");
            }
            _logger.LogInformation($"User id for sending: { user.Id.ToString()}");
            _vkApi.Messages.Send(new MessagesSendParams{ 
                RandomId = new DateTime().Millisecond,
                Message = message,
                UserId = user.Id
            });
        }
    }
}