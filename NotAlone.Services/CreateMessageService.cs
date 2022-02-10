using NotAlone.Models;
using System;

namespace NotAlone.Services
{
    public class CreateMessageService : ICreateMessageService
    {
        public string CreateMessage(LoverModel fromLover, LoverModel toLover)
        {
            string sex;
            if(toLover.LoverSex == "Мужчина")
            {
                sex = "он";
            }
            else
            {
                sex = "она";
            }

            string eventAim;
            if(toLover.EventAim == "Оба варианта")
            {
                eventAim = "найти отношения или дружбу";
            }
            else
            {
                eventAim = toLover.EventAim;
            }

            string faculty;
            if(toLover.Faculty == "Не БГУ")
            {
                faculty = "Давай посмотрим с какого он факультета! Ого, да твой партнер не из БГУ!";
            }
            else
            {
                faculty = $"Давай посмотрим с какого {sex} факультета. Ого, факультет - {toLover.Faculty}!";
            }

            string comment;
            if(toLover.PartnerComment == "")
            {
                comment = "Жаль, но тебе не оставили никакого послания.";
            }
            else
            {
                comment = $"Так-с, а тут для тебя ещё и послание оставили: \"{toLover.PartnerComment}\".";
            }

            string message = $"Я подобрал тебе пару! \n Имя твоего партнера - {toLover.Name} и его цель в этом мероприятии - {eventAim}.\n" +
                $"Ссылка на твоего партнера - {toLover.VkURL}. \n {faculty} \n Так-так, я тут заметил что {sex} - {toLover.Temperament}," +
                $" а по знаку {sex} - {toLover.ZodiakSign}. А рост то какой: {toLover.Height}, самое то, не находишь?\n" +
                $"Что же ещё тебе рассказать, хм. Отношение к алкоголю - {toLover.AlcoholRelation}, а к курению - {toLover.SmokeRelation}. \n" +
                $" {comment} \nНу и конечно, держи фото!";

            return message;
        }
    }
}