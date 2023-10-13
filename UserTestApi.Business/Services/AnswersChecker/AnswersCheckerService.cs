using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestApi.Business.Services
{
    public class AnswersCheckerService : IAnswersCheckerService
    {
        public int CheckAnswers(IEnumerable<CheckQuestion> questions, Dictionary<int, int> answers)
        {
            var questionsWithAnswers =
                answers.GroupJoin(
                    questions,
                    a => a.Key,
                    q => q.Number,
                    (a, q) => new { Q = q?.FirstOrDefault(), A = a.Value })
                .Where(qwa => qwa.Q != null);

            int points = 0;
            foreach (var question in questionsWithAnswers)
            {
                var answeredOption = question.Q!.Options.FirstOrDefault(o => o.Number == question.A);
                if (answeredOption != null)
                {
                    points += answeredOption.Points;
                }
            }

            return points;
        }
    }
}
