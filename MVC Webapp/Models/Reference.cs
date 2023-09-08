using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Reference
    {
        [Key]
        public int reference_id { get; set; }
        public string reference_name { get; set; }
        public string contact { get; set; }

        public int userId { get; set; }

    }
}
