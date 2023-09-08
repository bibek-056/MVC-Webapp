using System.ComponentModel.DataAnnotations;

namespace MVC_Webapp.Models
{
    public class Skills
    {
        [Key]
        public int skillId { get; set; }
        public string skill_name { get; set; }

        public int userId { get; set; }

    }
}
