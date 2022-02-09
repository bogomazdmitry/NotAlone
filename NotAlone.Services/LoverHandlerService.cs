using NotAlone.Models;

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
            // TODO: Realise conversion from string to LoverPeople
            return new LoverModel();
        }

        public void HandlePeople(LoverModel firstLoverModel, LoverModel secondLoverModel)
        {
            var messageForFirstLoverPeopleModel =
                _createMessageService.CreateMessage(secondLoverModel, firstLoverModel);
            var messageForSecondLoverPeopleModel =
                _createMessageService.CreateMessage(firstLoverModel, secondLoverModel);
            
            _vkService.SendMessage(messageForFirstLoverPeopleModel, ""); // TODO: insert link as recipient
            _vkService.SendMessage(messageForSecondLoverPeopleModel, ""); // TODO: insert link as recipient
        }

        public void HandlePeople(string firstLoverPeopleInfo, string secondLoverPeopleInfo)
        {
           var firstLoverPeople = LoverPeopleFromString(firstLoverPeopleInfo);
           var secondLoverPeople = LoverPeopleFromString(secondLoverPeopleInfo);    
           HandlePeople(firstLoverPeople, secondLoverPeople);
        }
    }
}