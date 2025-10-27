using Telegram.Bot;

public class TelegramBotService
{
    private readonly TelegramBotClient _botClient;
    private readonly long _adminChatId;

    public TelegramBotService()
    {
        string? token = Environment.GetEnvironmentVariable("TELEGRAM_TOKEN");
        string? adminId = Environment.GetEnvironmentVariable("TELEGRAM_ADMIN_CHATID");

        if (string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException("❌ TELEGRAM_TOKEN is missing.");
        
        if (string.IsNullOrWhiteSpace(adminId) || !long.TryParse(adminId, out _adminChatId))
            throw new InvalidOperationException("❌ TELEGRAM_ADMIN_CHATID is missing or invalid.");

        _botClient = new TelegramBotClient(token);
    }

    public async Task SendMessageAsync(string message)
    {
        await _botClient.SendMessage(_adminChatId, message);
    }
}