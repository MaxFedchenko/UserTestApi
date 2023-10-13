using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestApi.Business.Services
{
    public class Test
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int TotalPoints { get; set; }
        public required IEnumerable<Question> Questions { get; set; }
    }
    public class Question
    {
        public required int Number { get; set; }
        public required string Description { get; set; }
        public required IEnumerable<Option> Options { get; set; }
    }
    public class Option
    {
        public required int Number { get; set; }
        public required string Name { get; set; }
        public required int Points { get; set; }
    }
}
