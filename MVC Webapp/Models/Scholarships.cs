using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Scholarships
    {
        [Key]
        public int scholarship_id { get; set; }
        public string name { get; set; }
        public string institute { get; set; }

        public int userId { get; set; }

    }
}
