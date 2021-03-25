using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.Bot;//библиотека тг
using Telegram.Bot.Args; //чтобы получить тип сообщения
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using TelegramBot.Concious.Choice.Model;

namespace TelegramBot.Concious.Choice
{
    class Program
    {
        static TelegramBotClient Bot; //создали объект
        List<Deputy> Deputies { get; set; } = new List<Deputy>()
        {
            new Deputy()
            {
                Name = "Дастан Бекешев"
            }
        };

        List<Law> Laws { get; set; } = new List<Law>();

        static void Main(string[] args)
        {
            Bot = new TelegramBotClient("1691388722:AAGUr6FrDhXJ_L_TbZpfpKjZpG1eStMRn5c"); //ввели код от БотФазер

            Bot.OnMessage += Bot_OnMessageReceived; //события, чтобы получить смс, которое вел пользователь бота
            Bot.OnCallbackQuery += Bot_OnCallbackQueryReceived;

            var me = Bot.GetMeAsync().Result;//инфа про бота(имя, ник и тд)

            Console.WriteLine(me.FirstName);

            Bot.StartReceiving(); //чтобы начать получать смски пользователей(в тг которые пишут)
            Console.ReadLine();
            Bot.StopReceiving();//чтобы остановить смски пользователей для обработки
        }

        private static async void Bot_OnCallbackQueryReceived(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName } {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} нажал на кнопку {buttonText}");

            await Bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"вы нажали на кнопку{buttonText}");
        }

        private static async void Bot_OnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e) //появилось когда создали Bot.OnMessage
        {
            var message = e.Message;// смс от пользователя

            if (message == null || message.Type != MessageType.Text)
                return;

            Console.WriteLine($"{message.Text} от {message.From.FirstName}");

            switch(message.Text)
            {
                case "/start":
                    string text =
@"Привет, я бот conciousness
отправь имя депутата";
                    await /*чтобы обратывалось смс от неск-х юзеров*/ Bot.SendTextMessageAsync(message.From.Id/* находим отправителя по айПи*/, text /*отправляем текс*/);
                    break;
                case "Дастан Бекешев":
                    await Bot.SendTextMessageAsync(message.From.Id, @"голосовал против: 
1)Законопроект о манипулировании информацией в интернете
2)Законопроект о приостановлении действия некоторых норм конституционного закона «О выборах Президента КР и депутатов Жогорку Кенеша КР»

Инициатор:
1)Избирательный залог и избирательный порог в ходе выборов депутатов Жогорку Кенеша Кыргызской Республики
2)Совершенствование законодательства – помощь молодым специалистам
3)Мораторий на разработку и добычу урана на биосферной территории «Ыссык-Кёль»
4)Узаконить онлайн-петиции
");
                    break;
                case "Каныбек Иманалиев":
                    await Bot.SendTextMessageAsync(message.From.Id, @"Инициатор
1)Поправки по снижению избирательного барьера на парламентских выборах с 9 до 7% ");
                    break;
                case "":
                    await Bot.SendTextMessageAsync(message.From.Id, @"Инициатор
1)пустоты");
                    break;
                default:
                    await Bot.SendTextMessageAsync(message.From.Id, "введите Иф депутата");
                    break;


            }

        }

        public Deputy GetDeputy(string name)
        {
            return Deputies.Find(x => x.Name == name);
        }

        public List<Law> GetLaws(string lawName) //Не успели =(
        {
            return new List<Law>();
        }
    }
}
