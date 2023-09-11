namespace MVC_Webapp.DTOs.ProjectDTOs
{
    public class ProjectUpdateDTOs
    {
        public int project_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }

        public int userId { get; set; }
    }
}
