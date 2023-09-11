namespace MVC_Webapp.DTOs.InformationDTOs
{
    public class InformationUpdateDTOs
    {
        public int userId { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Int64 phone { get; set; }
        public string summary { get; set; }
        public string? github { get; set; }
        public string? linkedin { get; set; }
        public string? blog { get; set; }
        public string? designation { get; set; }
        public string address { get; set; }

    }
}
