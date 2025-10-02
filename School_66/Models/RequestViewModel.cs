public class RequestViewModel
{
    // Уникальный идентификатор запроса
    public int Id { get; set; }

    // Заголовок запроса (например: "Запит на відпустку")
    public string Title { get; set; }= string.Empty;

    // Тип запроса (Учень / Батьки / Інше)
    public string Type { get; set; } = string.Empty;

    // Дата создания запроса
    public DateTime CreatedAt { get; set; }

    // Статус запроса (Новий, Обробляється, Виконано)
    public string Status { get; set; }= string.Empty;

    // Можно добавить описание или комментарий
    public string Description { get; set; }= string.Empty;
}
