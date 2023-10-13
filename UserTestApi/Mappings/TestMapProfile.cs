using AutoMapper;
using UserTestApi.Business.Services;
using UserTestApi.DTOs;

namespace UserTestApi.Mappings
{
    public class TestMapProfile : Profile
    {
        public TestMapProfile() 
        {
            MapUserTest();
            MapTest();
        }

        private void MapUserTest() 
        {
            CreateMap<UserTest, UserTestDTO>();
        }
        private void MapTest() 
        {
            CreateMap<Option, OptionDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Test, TestDTO>();
        }
    }
}
