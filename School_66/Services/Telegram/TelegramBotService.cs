using Telegram.Bot;

namespace School_66.Service
{
    public class TelegramBotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly long _adminChatId = 6155527631; // <--- замени на свой Telegram ID

        public TelegramBotService()
        {
            string token = "8292821706:AAENpyNBsr1P_PK_fe5uHpR2LRKswYjfzlc"; // <--- замени на свой токен
            _botClient = new TelegramBotClient(token);
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                await _botClient.SendMessage(_adminChatId, message);
                Console.WriteLine("✅ Telegram message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Telegram error: {ex.Message}");
            }
        }
    }
}
