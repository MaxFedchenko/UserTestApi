namespace UserTestApi.Domain.Entities
{
    public class UserTestEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int? Points { get; set; }

        public UserEntity? User { get; set; }
        public TestEntity? Test { get; set; }
    }
}
