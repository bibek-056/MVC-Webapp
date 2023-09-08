using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Certifications
    {
        [Key]
        public int certificate_id { get; set; }
        public string title { get; set;}
        public string institute { get; set;}
        public string issued_date { get; set; }
        public string? link { get; set; }

        public int userId { get; set; }

    }
}
