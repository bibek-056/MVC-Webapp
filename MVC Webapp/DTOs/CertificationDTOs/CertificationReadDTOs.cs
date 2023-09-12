namespace MVC_Webapp.DTOs.CertificationDTOs
{
    public class CertificationReadDTOs
    {
        public int certificate_id { get; set; }
        public string title { get; set; }
        public string institute { get; set; }
        public string issued_date { get; set; }
        public string? link { get; set; }
    }
}
