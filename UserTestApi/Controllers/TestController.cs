using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UserTestApi.Business.Services;
using UserTestApi.DTOs;

namespace UserTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestController(ITestService testService, IMapper mapper) 
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserTestDTO[]), 200)]
        public async Task<IActionResult> Get([Required] string user)
        {
            var tests = await _testService.GetUserTests(user);
            return Ok(tests.Select(_mapper.Map<UserTest, UserTestDTO>));
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TestDTO), 200)]
        [ProducesResponseType(typeof(ErrorMessageDTO), 404)]
        public async Task<IActionResult> Get(int id) 
        {
            Test test;
            try
            {
                test = await _testService.GetTest(id);
            }
            catch (TestNotFoundException)
            {
                return NotFound(new ErrorMessageDTO { Error = "This test doesn't exist" });
            }

            return Ok(_mapper.Map<Test, TestDTO>(test));
        }
        [HttpPost("complete")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ErrorMessageDTO), 400)]
        public async Task<IActionResult> Post(CompleteTestDTO dto)
        {
            int points;
            try
            {
                points = await _testService.CompleteTest(dto.UserName, dto.TestId, dto.Answers);
            }
            catch(TestNotFoundException)
            {
                return BadRequest(new ErrorMessageDTO { Error = "This test doesn't exist or hasn't been assigned to the user" });
            }
            catch (TestAlreadyCompletedException)
            {
                return BadRequest(new ErrorMessageDTO { Error = "This test has already been completed by the user" });
            }

            return Ok(points);
        }
    }
}
