using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using UserTestApi.Domain.Entities;
using UserTestApi.Domain.Repositories;

namespace UserTestApi.Business.Services
{
    public class TestService : ITestService
    {
        private readonly IUserTestRepository _userTestRepository;
        private readonly ITestRepository _testRepository;
        private readonly IAnswersCheckerService _answersCheckerService;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public TestService(
            IUserTestRepository repository, 
            ITestRepository testRepository,
            IAnswersCheckerService answersCheckerService,
            IMemoryCache cache, 
            IMapper mapper)
        {
            _userTestRepository = repository;
            _testRepository = testRepository;
            _answersCheckerService = answersCheckerService;
            _cache = cache;
            _mapper = mapper;
        }

        private async Task<TestEntity> GetTestById(int testId)
        {
            var cacheKey = $"Test{testId}";

            if (_cache.TryGetValue(cacheKey, out TestEntity? entity))
                return entity!;

            entity = await _testRepository.Get(testId);

            if (entity == null)
                throw new TestNotFoundException();

            _cache.Set(cacheKey, entity);

            return entity;
        }

        public async Task<UserTest[]> GetUserTests(string user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return (await _userTestRepository.GetByUser(user))
                .Select(_mapper.Map<UserTestSelect, UserTest>)
                .ToArray();
        }
        public async Task<Test> GetTest(int testId)
        {
            var entity = await GetTestById(testId);

            return _mapper.Map<TestEntity, Test>(entity);
        }
        public async Task<int> CompleteTest(string user, int testId, Dictionary<int, int> answers)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userTestEntity = await _userTestRepository.Get(user, testId);

            if (userTestEntity == null)
                throw new TestNotFoundException();
            if (userTestEntity.Points != null)
                throw new TestAlreadyCompletedException();

            var testEntity = await GetTestById(testId);

            int points = _answersCheckerService.CheckAnswers(
                testEntity.Questions.Select(_mapper.Map<QuestionEntity, CheckQuestion>), 
                answers);

            _userTestRepository.UpdatePoints(userTestEntity.Id, points);
            await _userTestRepository.SaveChanges();

            return points;
        }
    }
}
