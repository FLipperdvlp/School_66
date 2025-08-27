namespace School_66.Enums;

/// <summary>
///active – ученик учится сейчас (основное состояние).
///inactive – ученик временно неактивен (например, в академическом отпуске).
///graduated – окончил школу.
///expelled – отчислен.
///transferred – переведен в другую школу.
/// </summary>
public enum StudentStatus
{
    active,
    inactive,
    graduated,
    expelled,
    transferred
}