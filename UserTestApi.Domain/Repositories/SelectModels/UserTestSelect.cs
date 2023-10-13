using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestApi.Domain.Repositories
{
    public class UserTestSelect
    {
        public required int TestId { get; set; }
        public required string TestName { get; set; }
        public required int TotalPoints { get; set; }
        public required int? UserPoints { get; set; }
    }
}
