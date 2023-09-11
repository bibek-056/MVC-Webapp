using AutoMapper;
using MVC_Webapp.Models;
using MVC_Webapp.DTOs.InformationDTOs;
using MVC_Webapp.DTOs.EducationDTOs;
using MVC_Webapp.DTOs.ExperienceDTOs;
using MVC_Webapp.DTOs.ProjectDTOs;
using MVC_Webapp.DTOs.SkillDTOs;

namespace MVC_Webapp.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles() {
            CreateMap< Information, InformationReadDTOs>();
            CreateMap<InformationCreateDTOs, Information>();
            CreateMap<InformationUpdateDTOs, Information>();
            CreateMap<Information,  InformationUpdateDTOs>();

            //Educations
            CreateMap<Educations, EducationReadDTOs>();
            CreateMap<EducationCreateDTOs, Educations>();
            CreateMap<EducationUpdateDTOs, Educations>();
            CreateMap<Educations, EducationUpdateDTOs>();

            //Experience
            CreateMap<Experiences, ExperienceReadDTOs>();
            CreateMap<ExperienceCreateDTOs, Experiences>();
            CreateMap<ExperienceUpdateDTOs, Experiences>();
            CreateMap<Experiences, ExperienceUpdateDTOs>();

            //Projects
            CreateMap<Projects, ProjectReadDTOs>();
            CreateMap<ProjectCreateDTOs, Projects>();
            CreateMap<ProjectUpdateDTOs, Projects>();
            CreateMap<Projects, ProjectUpdateDTOs>();

            //Skills
            CreateMap<Skills, SkillReadDTOs>();
            CreateMap<SkillCreateDTOs, Skills>();
            CreateMap<SkillUpdateDTOs, Skills>();
            CreateMap<Skills, SkillUpdateDTOs>();


        }
    }
}
