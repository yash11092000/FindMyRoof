
namespace PhysioWeb.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string ForRole { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public int NotiCount { get; set; }

        public List<string> Notifications { get; set; }

        public Notification()
        {
            Notifications = new List<string>();


        }
    }
}
