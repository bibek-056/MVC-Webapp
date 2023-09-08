using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Educations
    {
        [Key]
        public int education_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string board { get; set; }
        public string degree { get; set; }

        public int userId { get; set; }

    }
}
