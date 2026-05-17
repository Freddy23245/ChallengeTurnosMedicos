namespace TurnosMedicos.Helpers;

public static class DateTimeExtensions
{
    public static bool IsWithinCancellationWindow(this DateTime fechaTurno)
    {
        return (fechaTurno - DateTime.Now).TotalHours <= 24;
    }
}
