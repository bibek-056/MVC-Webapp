using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Experiences
    {
        [Key]
        public int experience_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string position { get; set; }
        public string tech { get; set; }
        public int userId { get; set; }

    }
}
