using System.ComponentModel.DataAnnotations;

namespace InfoPortalWeb.Models
{
    public class FAQ
    {
        [Key]
        public int ID { get; set; }
        public string Quesiton { get; set; }
        public string Answer { get; set; }

    }
}
