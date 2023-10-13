namespace UserTestApi.Domain.Entities
{
    public class UserEntity : BaseEntity<int>
    {
        public string Name { get; set; } = null!;

        public List<UserTestEntity>? Tests { get; set; }
    }
}
