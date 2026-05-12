namespace HotelReservationSystem.Utilities
{
    public class NotificationUtility
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine("[NOTIFICATION]");
            Console.WriteLine("To: " + recipient);
            Console.WriteLine("Message: " + message);
        }
    }
}