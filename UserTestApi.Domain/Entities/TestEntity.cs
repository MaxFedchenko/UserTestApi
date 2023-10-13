using System.ComponentModel.DataAnnotations;

namespace UserTestApi.Domain.Entities
{
    public class TestEntity : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public int TotalPoints { get; set; }
        [Required]
        public List<QuestionEntity> Questions { get; set; } = new();

        public List<UserTestEntity>? Users { get; set; }
    }
    public class QuestionEntity
    {
        public required int Number { get; set; }
        public required string Description { get; set; }
        public required List<OptionEntity> Options { get; set; }
    }
    public class OptionEntity
    {
        public required int Number { get; set; }
        public required string Name { get; set; }
        public required int Points { get; set; }
    }
}
