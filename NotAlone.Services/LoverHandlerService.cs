using NotAlone.Models;
using System.Threading.Tasks;

namespace NotAlone.Services
{
    public class LoverHandlerService : ILoverHandlerService
    {
        private readonly IVkService _vkService;

        private readonly ICreateMessageService _createMessageService;

        public LoverHandlerService(IVkService vkService, ICreateMessageService createMessageService)
        {
            _vkService = vkService;
            _createMessageService = createMessageService;
        }

        public LoverModel LoverPeopleFromString(string loverPeopleString)
        {
            string[] loverArr = loverPeopleString.Split('\t');
            var model = new LoverModel()
            {
                Date = loverArr[0],
                Email = loverArr[1],
                Name = loverArr[2],
                EventAim = loverArr[3],
                VkURL = loverArr[4],
                LoverSex = loverArr[5],
                PartnerSex = loverArr[6],
                Faculty = loverArr[7],
                Adress = loverArr[8],
                Temperament = loverArr[9],
                ZodiakSign = loverArr[10],
                Height = loverArr[11],
                MusicPreferences = loverArr[12],
                Hobby = loverArr[13],
                AlcoholRelation = loverArr[14],
                SmokeRelation = loverArr[15],
                BadHabbits = loverArr[16],
                PartnerBadHabbits = loverArr[17],
                GoodHabbits = loverArr[18],
                PartnerComment = loverArr[19],
                BlindDate = loverArr[20],
                AdminComment = loverArr[21],
                PhotoUrl = loverArr[22]
            };
            return model;
        }

        public async Task HandlePeople(LoverModel firstLoverModel, LoverModel secondLoverModel)
        {
            var messageForFirstLoverPeopleModel =
                _createMessageService.CreateMessage(secondLoverModel, firstLoverModel);
            var messageForSecondLoverPeopleModel =
                _createMessageService.CreateMessage(firstLoverModel, secondLoverModel);
            
            await _vkService.SendMessageImage(messageForFirstLoverPeopleModel, secondLoverModel.VkURL, firstLoverModel.PhotoUrl); 
            await _vkService.SendMessageImage(messageForSecondLoverPeopleModel, firstLoverModel.VkURL, secondLoverModel.PhotoUrl);
        }

        public async Task HandlePeople(string firstLoverPeopleInfo, string secondLoverPeopleInfo)
        {
           var firstLoverPeople = LoverPeopleFromString(firstLoverPeopleInfo);
           var secondLoverPeople = LoverPeopleFromString(secondLoverPeopleInfo);    
           await HandlePeople(firstLoverPeople, secondLoverPeople);
        }
    }
}