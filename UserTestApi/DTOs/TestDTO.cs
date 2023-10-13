namespace UserTestApi.DTOs
{
    public class TestDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int TotalPoints { get; set; }
        public required IEnumerable<QuestionDTO> Questions { get; set; }
    }
    public class QuestionDTO
    {
        public required int Number { get; set; }
        public required string Description { get; set; }
        public required IEnumerable<OptionDTO> Options { get; set; }
    }
    public class OptionDTO
    {
        public required int Number { get; set; }
        public required string Name { get; set; }
    }
}
