namespace HotelReservationSystem.Utilities
{
    public class LoggingUtility
    {
        public void Log(string message)
        {
            Console.WriteLine("[LOG] " + DateTime.Now + " - " + message);
        }
    }
}