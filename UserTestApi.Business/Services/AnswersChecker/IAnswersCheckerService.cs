namespace UserTestApi.Business.Services
{
    public interface IAnswersCheckerService
    {
        int CheckAnswers(IEnumerable<CheckQuestion> questions, Dictionary<int, int> answers);
    }
}