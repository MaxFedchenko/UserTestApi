using AutoMapper;
using UserTestApi.Business.Services;
using UserTestApi.Domain.Entities;
using UserTestApi.Domain.Repositories;

namespace UserTestApi.Business.Mappings
{
    public class TestMapProfile : Profile
    {
        public TestMapProfile() 
        {
            MapUserTest();
            MapTest();
            MapCheckerQuestion();
        }

        private void MapUserTest() 
        {
            CreateMap<UserTestSelect, UserTest>()
                .ForMember(m => m.UserPoints, cfg => cfg.MapFrom(e => e.UserPoints ?? 0))
                .ForMember(m => m.IsCompleted, cfg => cfg.MapFrom(e => e.UserPoints != null));
        }
        private void MapTest() 
        {
            CreateMap<OptionEntity, Option>();
            CreateMap<QuestionEntity, Question>();
            CreateMap<TestEntity, Test>();
        }
        private void MapCheckerQuestion() 
        {
            CreateMap<Option, CheckOption>();
            CreateMap<Question, CheckQuestion>();
        }
    }
}
