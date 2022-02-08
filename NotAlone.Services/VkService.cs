using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NotAlone.Models;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace NotAlone.Services
{
    public class VkService : IVkService
    {
        private readonly IVkApi _vkApi;
        
        private readonly ILogger<VkService> _logger;

        private readonly IConfiguration _configuration;

        public VkService(IVkApi vkApi, ILogger<VkService> logger, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _logger = logger;
            _configuration = configuration;
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
        
        public string HandleRequest(VkApiRequest request)
        {
            if (request != null)
            {
                _logger.LogInformation("VkApiSettings:Confirmation: " + _configuration["VkApiSettings:Confirmation"]);
                switch (request?.Type)
                {
                    case "confirmation":
                        return _configuration["VkApiSettings:Confirmation"];
                    // TODO: Fix this part, because vk is stupid platform
                    case "message_new":
                    {
                        var message =  request.Object.ToObject<Message>();
                        _logger.LogInformation(message?.ChatId.ToString() ?? "ChatId is null");
                        if (message?.ChatId != null)
                        {
                            var messageHistory = _vkApi.Messages.GetHistory(new MessagesGetHistoryParams()
                            {
                                PeerId = message?.ChatId
                            });
                            _logger.LogInformation(messageHistory.Messages.Count().ToString() ?? "Message count is null");
                            _logger.LogInformation(messageHistory.Messages.FirstOrDefault()?.Id.ToString() ?? "messageHistory.Messages.FirstOrDefault()?.Id is null");
                            if (messageHistory.Messages.Count() == 1 
                                && messageHistory.Messages.FirstOrDefault()?.Id == message?.Id)
                            {
                                _vkApi.Messages.Send(new MessagesSendParams{ 
                                    RandomId = new DateTime().Millisecond,
                                    Message = _configuration["MessageTemplates:FirstMessageTemplate"],
                                    PeerId = message?.ChatId
                                });
                            }
                        }
                        return "ok";
                    }
                }
            }
            else
            {
                _logger.LogInformation("Empty request");
            }

            return "ok";
        }
    }
}