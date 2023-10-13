using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestApi.Domain.EF;
using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public class UserTestRepository : BaseRepository<UserTestEntity, int>, IUserTestRepository
    {
        public UserTestRepository(UserTestsContext context) : base(context) { }

        public async Task<UserTestSelect[]> GetByUser(string user)
        {
            return await Context.UserTests
                .Where(ut => ut.User!.Name == user)
                .Include(ut => ut.Test)
                .Select(ut => new UserTestSelect
                {
                    TestId = ut.TestId,
                    TestName = ut.Test!.Name,
                    TotalPoints = ut.Test!.TotalPoints,
                    UserPoints = ut.Points
                })
                .ToArrayAsync();
        }
        public async Task<UserTestEntity?> Get(string user, int testId)
        {
            return await Context.UserTests
                .AsNoTracking()
                .Where(ut => ut.User!.Name == user)
                .Where(ut => ut.TestId == testId)
                .FirstOrDefaultAsync();
        }
        public void UpdatePoints(int userTestId, int points) 
        {
            Context.Entry(new UserTestEntity { Id = userTestId, Points = points })
                .Property(ut => ut.Points).IsModified = true;
        }
    }
}
