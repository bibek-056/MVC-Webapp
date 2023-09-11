namespace MVC_Webapp.DTOs.EducationDTOs
{
    public class EducationUpdateDTOs
    {
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
