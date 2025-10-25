using Telegram.Bot;

namespace School_66.Service
{
    public class TelegramBotService
    {
        private readonly TelegramBotClient _botClient;
        private readonly long _adminChatId;

        public TelegramBotService()
        {
            string? token = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
            string? adminId = Environment.GetEnvironmentVariable("TELEGRAM_ADMIN_CHATID");

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException("❌ TELEGRAM_TOKEN is not set in environment variables.");
            }

            if (string.IsNullOrWhiteSpace(adminId) || !long.TryParse(adminId, out _adminChatId))
            {
                throw new InvalidOperationException("❌ TELEGRAM_ADMIN_CHATID is missing or invalid.");
            }

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
