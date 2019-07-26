using AutoMapper;
using CareebizExam.DTO;
using CareebizExam.Model;

namespace CareebizExam.Logic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
            : this("MyProfile")
        {
        }
        protected MappingProfile(string profileName)
            : base(profileName)
        {
            
            CreateMap<Shapes, ShapesDTO>();
        }
    }

}
