using System.ComponentModel.DataAnnotations;

namespace UserTestApi.DTOs
{
    public class CompleteTestDTO
    {
        [Required]
        public required int TestId { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required Dictionary<int, int> Answers { get; set; }
    }
}
