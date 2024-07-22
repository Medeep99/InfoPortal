using System.ComponentModel.DataAnnotations;

namespace InfoPortalWeb.Models
{
    public class FeedbackInfo
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string FeedbackMessage { get; set; }
    }
}
