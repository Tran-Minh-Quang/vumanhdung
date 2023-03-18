using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Team12EUP.Data;
using Team12EUP.DTO;
using Team12EUP.Entity;

namespace Team12EUP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VideoController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromBody] AddVideo rq)
        {
            rq.video.Id = Guid.NewGuid();
            var mapvideo = _mapper.Map<Video>(rq.video);
            await _context.videos.AddAsync(mapvideo);
            rq.test.Id = Guid.NewGuid();
            rq.test.VideoId = mapvideo.Id;
            rq.test.TotalQuestion= rq.questions.Count();
            var mapTest = _mapper.Map<Test>(rq.test);
            await _context.tests.AddAsync(mapTest);
            foreach (var item in rq.questions)
            {
                item.Id = Guid.NewGuid();
                item.TestId = mapTest.Id;
                var mapquestions = _mapper.Map<Question>(item);
                await _context.questions.AddAsync(mapquestions);
            }
            return Ok(mapvideo.Id);
        }

    }
}
