using Telegram.Bot;

namespace School_66.Service
{
    public class TelegramBotService
    {
        private readonly TelegramBotClient _botClient;
        long adminChatId = long.Parse(Environment.GetEnvironmentVariable("TELEGRAM_ADMIN_CHATID")!);

        public TelegramBotService()
        {
            string? token = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN")!;
            _botClient = new TelegramBotClient(token);
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                await _botClient.SendMessage(adminChatId, message);
                Console.WriteLine("✅ Telegram message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Telegram error: {ex.Message}");
            }
        }
    }
}
