using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;

using UserTestApi.Business.Services;
using UserTestApi.Domain.Entities;

using UserTestApi.Domain.Repositories;

namespace UserTestApi.Tests
{
    public class TestServiceTests
    {
        private readonly Mock<IUserTestRepository> userTestRepositoryMock = new();
        private readonly Mock<ITestRepository> testRepositoryMock = new();
        private readonly Mock<IAnswersCheckerService> answersCheckerServiceMock = new();
        private readonly MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private readonly Mock<IMapper> mapper = new();

        [Fact]
        public async Task CompleteTest_ValidInput_ReturnsPointsAndUpdatesUserTest()
        {
            // Arrange
            var answers = new Dictionary<int, int>();
            var user = "testuser";
            var testId = 1;
            var points = 10;
            var userTestEntity = new UserTestEntity
            {
                Id = 1,
                Points = null // User has not completed the test yet
            };
            var testEntity = new TestEntity { };

            userTestRepositoryMock.Setup(repo => repo.Get(user, testId))
                .ReturnsAsync(userTestEntity);

            testRepositoryMock.Setup(repo => repo.Get(testId))
                .ReturnsAsync(testEntity);

            answersCheckerServiceMock.Setup(service => service.CheckAnswers(
                It.IsAny<IEnumerable<CheckQuestion>>(), It.IsAny<Dictionary<int, int>>()))
                .Returns(points);

            mapper.Setup(m => m.Map<Question, CheckQuestion>(It.IsAny<Question>())).Returns((CheckQuestion)null!);

            var service = new TestService(
                userTestRepositoryMock.Object,
                testRepositoryMock.Object,
                answersCheckerServiceMock.Object,
                cache,
                mapper.Object);

            // Act
            var result = await service.CompleteTest(user, testId, answers);

            // Assert
            userTestRepositoryMock.Verify(repo => repo.Get(user, testId), Times.Once);

            testRepositoryMock.Verify(repo => repo.Get(testId), Times.Once);

            answersCheckerServiceMock.Verify(serv => serv.CheckAnswers(
                It.IsAny<IEnumerable<CheckQuestion>>(), It.IsAny<Dictionary<int, int>>()), Times.Once);

            userTestRepositoryMock.Verify(repo => repo.UpdatePoints(userTestEntity.Id, points), Times.Once);

            userTestRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task CompleteTest_UserTestHasPoints_ThrowsTestAlreadyCompletedException()
        {
            // Arrange
            var answers = new Dictionary<int, int>();
            var user = "testuser";
            var testId = 1;
            var points = 10;
            var userTestEntity = new UserTestEntity
            {
                Id = 1,
                Points = points // User has completed the test with these results
            };

            userTestRepositoryMock.Setup(repo => repo.Get(user, testId))
                .ReturnsAsync(userTestEntity);

            var service = new TestService(
                userTestRepositoryMock.Object,
                testRepositoryMock.Object,
                answersCheckerServiceMock.Object,
                cache,
                mapper.Object);

            // Act and Assert
            await Assert.ThrowsAsync<TestAlreadyCompletedException>(() => service.CompleteTest(user, testId, answers));
        }
    }
}
