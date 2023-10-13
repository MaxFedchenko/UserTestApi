namespace UserTestApi.DTOs
{
    public class UserTestDTO
    {
        public required int TestId { get; set; }
        public required string TestName { get; set; }
        public required bool IsCompleted { get; set; }
        public required int TotalPoints { get; set; }
        public required int UserPoints { get; set; }
    }
}
