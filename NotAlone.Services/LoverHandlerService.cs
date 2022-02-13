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

        public LoverPropertiesModel LoverPeopleFromString(string loverPeopleString)
        {
            string[] loverArr = loverPeopleString.Split('\t');
            var model = new LoverPropertiesModel()
            {
                Date = loverArr[0],
                Email = loverArr[1],
                Name = loverArr[2],
                EventAim = loverArr[3],
                VKURL = loverArr[4],
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

        public async Task HandlePeople(LoverPropertiesModel firstLoverModel, LoverPropertiesModel secondLoverModel)
        {
            var messageForFirstLoverPeopleModel =
                _createMessageService.CreateMessage(secondLoverModel, firstLoverModel);
            var messageForSecondLoverPeopleModel =
                _createMessageService.CreateMessage(firstLoverModel, secondLoverModel);
            
            await _vkService.SendMessageWithImage(messageForFirstLoverPeopleModel, secondLoverModel.VKURL, firstLoverModel.PhotoUrl); 
            await _vkService.SendMessageWithImage(messageForSecondLoverPeopleModel, firstLoverModel.VKURL, secondLoverModel.PhotoUrl);
        }

        public async Task HandlePeopleBlind(LoverPropertiesModel firstLoverModel, LoverPropertiesModel secondLoverModel, string linkBlindDate)
        {
            var messageForFirstLoverPeopleModel =
                _createMessageService.CreateMessageForBlindDate(secondLoverModel, firstLoverModel, linkBlindDate);
            var messageForSecondLoverPeopleModel =
                _createMessageService.CreateMessageForBlindDate(firstLoverModel, secondLoverModel, linkBlindDate);

            await _vkService.SendMessage(messageForFirstLoverPeopleModel, secondLoverModel.VKURL);
            await _vkService.SendMessage(messageForSecondLoverPeopleModel, firstLoverModel.VKURL);
        }

        // TODO: check link for blind dates != null
        // if link != null but Checker false throw exception
        // if Checker == true and link == null throw exception
        public async Task HandlePeople(string firstLoverPeopleInfo, string secondLoverPeopleInfo, bool blindDateChecker, string linkBlindDate)
        {
           var firstLoverPeople = LoverPeopleFromString(firstLoverPeopleInfo);
           var secondLoverPeople = LoverPeopleFromString(secondLoverPeopleInfo);
            if (blindDateChecker)
            {
                await HandlePeopleBlind(firstLoverPeople, secondLoverPeople, linkBlindDate);
            }
            else
            {
                await HandlePeople(firstLoverPeople, secondLoverPeople);
            }
        }
    }
}