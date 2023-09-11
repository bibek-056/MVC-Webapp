namespace MVC_Webapp.DTOs.ExperienceDTOs
{
    public class ExperienceCreateDTOs
    { 
        public string name { get; set; }
        public string address { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string position { get; set; }
        public string tech { get; set; }
        public int userId { get; set; }
    }
}
