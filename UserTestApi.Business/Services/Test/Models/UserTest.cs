using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestApi.Business.Services
{
    public class UserTest
    {
        public required int TestId { get; set; }
        public required string TestName { get; set; }
        public required bool IsCompleted { get; set; }
        public required int TotalPoints { get; set; }
        public required int UserPoints { get; set; }
    }
}
