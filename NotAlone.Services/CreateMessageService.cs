using NotAlone.Models;
using System;

namespace NotAlone.Services
{
    public class CreateMessageService : ICreateMessageService
    {
        // TODO: change first 3 methods to private
        public string ReturnComment(LoverPropertiesModel model)
        {
            string comment;
            if (model.PartnerComment == "")
            {
                comment = "Жаль, но тебе не оставили никакого послания.";
            }
            else
            {
                comment = $"Так-с, а тут для тебя ещё и послание оставили: \"{model.PartnerComment}\".";
            }
            return comment;
        }

        public string ReturnFaculty(LoverPropertiesModel model, string secondPartnerSexPropertries)
        {
            string faculty;
            if (model.Faculty == "Не БГУ")
            {
                faculty = "Давай посмотрим с какого он факультета! Ого, да твой партнер не из БГУ!";
            }
            else
            {
                faculty = $"Давай посмотрим с какого {secondPartnerSexPropertries} факультета. Ого, факультет - {model.Faculty}!";
            }
            return faculty;
        }

        public string ReturnEventAim(LoverPropertiesModel model)
        {
            string eventAim;
            if (model.EventAim == "Оба варианта")
            {
                eventAim = "найти отношения или дружбу";
            }
            else
            {
                eventAim = model.EventAim;
            }
            return eventAim;
        }

        public string CreateMessage(LoverPropertiesModel fromLover, LoverPropertiesModel toLover)
        {
            string[] firstPartnerSexProperties = new string[3];
            if (fromLover.LoverSex == "Мужчина")
            {
                firstPartnerSexProperties[0] = "заполнял";
                firstPartnerSexProperties[1] = "доверился";
                firstPartnerSexProperties[2] = "доволен";
            }
            else
            {
                firstPartnerSexProperties[0] = "заполняла";
                firstPartnerSexProperties[1] = "доверилась";
                firstPartnerSexProperties[2] = "довольна";
            }

            string secondPartnerSexPropertries;
            if(toLover.LoverSex == "Мужчина")
            {
                secondPartnerSexPropertries = "он";
            }
            else
            {
                secondPartnerSexPropertries = "она";
            }

            string message = $"Привет! Совсем недавно ты {firstPartnerSexProperties[0]} анкету, чтобы провести этот праздник по - особенному. Я очень рад, что ты {firstPartnerSexProperties[1]} мне!\n" +
                $"Я постарался подобрать тебе идеального партнера. Надеюсь ты останешься {firstPartnerSexProperties[2]}" + $"\n Имя твоего партнера - {toLover.Name} и его цель в этом мероприятии - {ReturnEventAim(toLover)}.\n" +
                $"Ссылка на твоего партнера - {toLover.VKURL}. \n {ReturnFaculty(toLover, secondPartnerSexPropertries)} \n Так-так, я тут заметил что {secondPartnerSexPropertries} - {toLover.Temperament}," +
                $" а по знаку {secondPartnerSexPropertries} - {toLover.ZodiakSign}. А рост то какой: {toLover.Height}, самое то, не находишь?\n" +
                $"Что же ещё тебе рассказать, хм. Отношение к алкоголю - {toLover.AlcoholRelation}, а к курению - {toLover.SmokeRelation}. \n" +
                $" {ReturnComment(toLover)} \nНу и конечно, держи фото! \n Скорее начинайте общаться, а после обязательно оставь отзыв, ведь так мы сможем улучшить нашу дальнейшую работу!\n Удачи! ❤";

            return message;
        }

        public string CreateMessageForBlindDate(LoverPropertiesModel fromLover, LoverPropertiesModel toLover, string linkBlindDate)
        {
            string[] firstPartnerSexProperties = new string[3];
            if (fromLover.LoverSex == "Мужчина")
            {
                firstPartnerSexProperties[0] = "заполнял";
                firstPartnerSexProperties[1] = "доверился";
                firstPartnerSexProperties[2] = "смог";
            }
            else
            {
                firstPartnerSexProperties[0] = "заполняла";
                firstPartnerSexProperties[1] = "доверилась";
                firstPartnerSexProperties[2] = "смогла";
            }

            string[] secondPartnerSexPropertries = new string[3];
            if (toLover.LoverSex == "Мужчина")
            {
                secondPartnerSexPropertries[0] = "он";
                secondPartnerSexPropertries[1] = "него";
                secondPartnerSexPropertries[2] = "его";
            }
            else
            {
                secondPartnerSexPropertries[0] = "она";
                secondPartnerSexPropertries[1] = "неё";
                secondPartnerSexPropertries[2] = "её";
            }


            string message = $"Привет! Совсем недавно ты {firstPartnerSexProperties[0]} анкету, чтобы провести этот праздник по - особенному. Я очень рад, что ты {firstPartnerSexProperties[1]} мне!\n" +
                $"Ты настоящий смельчак, ведь {firstPartnerSexProperties[2]} рискнуть и полностью положиться на мой вкус. Я выбрал для тебя идеального партнера и назначил вам тайную встречу! \n Расскажу про {secondPartnerSexPropertries[1]} подробнее:" +
                $"\n Имя твоего партнера - {toLover.Name} и {secondPartnerSexPropertries[2]} цель в этом мероприятии - {ReturnEventAim(toLover)}.\n" +
                $"{ReturnFaculty(toLover, secondPartnerSexPropertries[0])} \n Так-так, я тут заметил что {secondPartnerSexPropertries[0]} - {toLover.Temperament}," +
                $" а по знаку {secondPartnerSexPropertries[0]} - {toLover.ZodiakSign}. А рост то какой: {toLover.Height}, самое то, не находишь?\n" +
                $"Что же ещё тебе рассказать, хм. Отношение к алкоголю - {toLover.AlcoholRelation}, а к курению - {toLover.SmokeRelation}. \n" +
                $"Ссылка на комнату для вашего анонимного общения, чтобы вы могли договориться о месте проведения слепого свидания {linkBlindDate} \n" +
                $" {ReturnComment(toLover)} \n А чтобы ты наверняка понял, что нашел нужного человека, предлагаю тебе принести с собой валентинку красного цвета ❤ (возьми ее в руки, чтобы ее было видно). \n" +
                $" После встречи оставь отзыв, ведь так мы сможем улучшить нашу дальнейшую работу! \n Удачи! ❤";

            return message;
        }
    }
}