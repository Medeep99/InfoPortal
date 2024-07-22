using System.ComponentModel.DataAnnotations;

namespace InfoPortalWeb.Models
{
    public class Content
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
