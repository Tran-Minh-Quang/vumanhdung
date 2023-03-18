using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
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
        public class CreateAdvertísementDTO
        {
            [JsonIgnore]
            public Guid id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
        }
        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] CreateAdvertísementDTO rq)
        {
            rq.id=Guid.NewGuid();
           var map =_mapper.Map<Advertisement>(rq);  
            await _context.advertisements.AddAsync(map);
            await _context.SaveChangesAsync();
            return Ok(map.Id);

        }
        public class HistoryTestDTO
        {
            [JsonIgnore]
            public Guid id { get; set; }
            [JsonIgnore]
            public float Mark { get; set; }
            [JsonIgnore]
            public DateTime Date { get; set; }
            public Guid UserId { get; set; }
            public List<QuestionDTOs> Questions { get; set; }
        }
        public class QuestionDTOs
        {
            public Guid QuestionId { get; set; }
            public int Answer { get; set; }
        }
        [HttpPost("AddHistoryTest")]
        public async Task<IActionResult> AddHistoryTest([FromBody] HistoryTestDTO rq)
        {
            int check = 0;
            foreach(var item in rq.Questions)
            {
                var checkitem = await _context.questions.FirstOrDefaultAsync(i => i.Id == item.QuestionId);
                if(item.Answer==checkitem.Answer)
                {
                    check++;
                }    
            }    
            rq.id = Guid.NewGuid();
            rq.Mark = check/(rq.Questions.Count());
            rq.Date = DateTime.Now;
            var map = _mapper.Map<HistoryTest>(rq);
            await _context.historyTests.AddAsync(map);
            await _context.SaveChangesAsync();
            return Ok(map);

        }
        public class RankDTO
        {
            public Guid UserId { get; set;}
            public float Mark { get; set; }
        }
        [HttpGet("Ranking")]
        public async Task<IActionResult>Ranking()
        {
            var join = from s in _context.historyTests
                       join st in _context.users on s.UserId equals st.Id into tmp
                       from st in tmp.DefaultIfEmpty()
                       select new RankDTO
                       {
                           UserId = st.Id,
                           Mark = s.Mark

                       };
            var value = join.GroupBy(n => n.UserId).Select(g => new RankDTO
            {
                UserId = g.Key,
                Mark = g.Sum(n => n.Mark),
            }).OrderByDescending(a=>a.Mark).ToList();
            return Ok(value);
        }


    }
}
