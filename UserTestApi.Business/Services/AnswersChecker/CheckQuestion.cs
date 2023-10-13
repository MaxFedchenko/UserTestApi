using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTestApi.Business.Services
{
    public class CheckQuestion
    {
        public required int Number { get; set; }
        public required IEnumerable<CheckOption> Options { get; set; }
    }
    public class CheckOption
    {
        public required int Number { get; set; }
        public required int Points { get; set; }
    }
}
