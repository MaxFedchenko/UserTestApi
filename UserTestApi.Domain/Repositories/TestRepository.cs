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
    public class TestRepository : BaseRepository<TestEntity, int>, ITestRepository
    {
        public TestRepository(UserTestsContext context) : base(context) { }
    }
}
