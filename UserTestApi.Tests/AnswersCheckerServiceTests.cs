using UserTestApi.Business.Services;

namespace UserTestApi.Tests
{
    public class AnswersCheckerServiceTests
    {
        private readonly CheckQuestion[] _questions = new[]
        {
            new CheckQuestion { Number = 1, Options = new[] { new CheckOption { Number = 1, Points = 2 } } },
            new CheckQuestion { Number = 2, Options = new[] { new CheckOption { Number = 1, Points = 3 } } }
        };

        [Fact]
        public void CheckAnswers_CorrectAnswers_ReturnsExpectedPoints()
        {
            // Arrange
            var answers = new Dictionary<int, int>
            {
                { 1, 1 },
                { 2, 1 } 
            };

            var service = new AnswersCheckerService();

            // Act
            int result = service.CheckAnswers(_questions, answers);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void CheckAnswers_IncorrectAnswers_ReturnsZeroPoints()
        {
            // Arrange
            var answers = new Dictionary<int, int>
            {
                { 1, 2 },
                { 2, 3 }
            };

            var service = new AnswersCheckerService();

            // Act
            int result = service.CheckAnswers(_questions, answers);

            // Assert
            Assert.Equal(0, result);
        }


        [Fact]
        public void CheckAnswers_InvalidAnswers_ReturnsExpectedPoints()
        {
            // Arrange
            var answers = new Dictionary<int, int>
            {
                { 3, -1 },
                { -1, 1 },
                { 2, 1 }
            };

            var service = new AnswersCheckerService();

            // Act
            int result = service.CheckAnswers(_questions, answers);

            // Assert
            Assert.Equal(3, result);
        }
    }
}