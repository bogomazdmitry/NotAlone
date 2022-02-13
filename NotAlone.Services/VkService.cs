using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        private async Task<string> UploadFile(string serverUrl, string file, string fileExtension)
        {
            var data = GetBytes(file);

            using (var client = new HttpClient())
            {
                var requestContent = new MultipartFormDataContent();
                var content = new ByteArrayContent(data);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                requestContent.Add(content, "file", $"file.{fileExtension}");

                var response = client.PostAsync(serverUrl, requestContent).Result;
                return Encoding.Default.GetString(await response.Content.ReadAsByteArrayAsync());
            }
        }

        private byte[] GetBytes(string fileUrl)
        {
            using (var webClient = new WebClient())
            {
                return webClient.DownloadData(fileUrl);
            }
        }

        private VkNet.Model.User ReturnUser(string recipientUrl)
        {
            var recipientId = Regex.Split(recipientUrl, "/|@")
                .Last(e => !string.IsNullOrEmpty(e));

            var user = _vkApi.Users.Get(new List<string> {
                recipientId
            }, ProfileFields.Uid).FirstOrDefault();
            if (user == null)
            {
                throw new Exception($"Пользователь с id: {recipientId} не найден");
            }
            return user;
        }

        public async Task SendMessageWithImage(string message, string recipient, string imageUrl)
        {
            var user = ReturnUser(recipient);
            
            _logger.LogInformation($"User id for sending: { user.Id.ToString()}");
            var imageId = imageUrl.Split("=")[1];

            imageUrl = $"https://drive.google.com/u/0/uc?id=" + imageId + "&export=download";
            var uploadServer = _vkApi.Photo.GetMessagesUploadServer(200654480);
            var response = await UploadFile(uploadServer.UploadUrl, imageUrl, "jpg");
            var attachment = _vkApi.Photo.SaveMessagesPhoto(response);

            _vkApi.Messages.Send(new MessagesSendParams{ 
                RandomId = new DateTime().Millisecond,
                Message = message,
                UserId = user.Id,
                Attachments = attachment
            });
        }

        public async Task SendMessage(string message, string recipient)
        {
            var user = ReturnUser(recipient);

            _logger.LogInformation($"User id for sending: { user.Id.ToString()}");
            
            _vkApi.Messages.Send(new MessagesSendParams
            {
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