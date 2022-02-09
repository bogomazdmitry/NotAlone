using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _configuration;

        public VkService(IVkApi vkApi, ILogger<VkService> logger, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _logger = logger;
            _configuration = configuration;
        }
        
        public void SendMessage(string message, string recipient)
        {
            var recipientId = Regex.Split(recipient, "/|@")
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
        
        public string HandleRequest(JsonElement request)
        {
            _logger.LogInformation(request.ToString());

            switch (request.GetProperty("type").ToString())
            {
                case "confirmation":
                {
                    _logger.LogInformation("VkApiSettings:Confirmation: " + _configuration["VkApiSettings:Confirmation"]);
                    return _configuration["VkApiSettings:Confirmation"];
                }
                case "message_new":
                {
                    var messageObject = request.GetProperty("object").GetProperty("message");
                    var chatId = Int64.Parse(messageObject.GetProperty("from_id").ToString());
                    var messageId = Int64.Parse(messageObject.GetProperty("id").ToString());
                    
                    var messageHistory = _vkApi.Messages.GetHistory(new MessagesGetHistoryParams()
                    {
                        PeerId = chatId,
                        Reversed = true
                    });
                    _logger.LogInformation(messageHistory.Messages.Count().ToString() ?? "Message count is null");
                    _logger.LogInformation(messageHistory.Messages.FirstOrDefault()?.Id.ToString() ?? "messageHistory.Messages.FirstOrDefault()?.Id is null");
                    if (messageHistory.Messages.FirstOrDefault()?.Id == messageId)
                    {
                        _vkApi.Messages.Send(new MessagesSendParams{ 
                            RandomId = new DateTime().Millisecond,
                            Message = _configuration["MessageTemplates:FirstMessageTemplate"],
                            PeerId = chatId
                        });
                    }
                    return "ok";
                }
            }
            
            return "ok";
        }
    }
}