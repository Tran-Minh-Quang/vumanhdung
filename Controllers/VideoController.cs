using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
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
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] AddCoursedDTO rq)
        {
            rq.Id = Guid.NewGuid();
            var map = _mapper.Map<Course>(rq);
            await _context.courses.AddAsync(map);
            foreach (var item in rq.addVideo)
            {
                item.video.Id = Guid.NewGuid();
                item.video.CourseId = map.Id;
                var mapvideo = _mapper.Map<Video>(item.video);
                await _context.videos.AddAsync(mapvideo);
                item.test.Id = Guid.NewGuid();
                item.test.VideoId = mapvideo.Id;

                item.test.TotalQuestion = item.questions.Count();
                var mapTest = _mapper.Map<Test>(item.test);
                await _context.tests.AddAsync(mapTest);
                foreach (var item1 in item.questions)
                {
                    item1.Id = Guid.NewGuid();
                    item1.TestId = mapTest.Id;
                    var mapquestions = _mapper.Map<Question>(item1);
                    await _context.questions.AddAsync(mapquestions);
                }
            }
            await _context.SaveChangesAsync();
            return Ok(map.Id);
        }
        public class AddCoursedDTO
        {
            [JsonIgnore]
            public Guid Id { get; set; }

            public Guid UserId { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public List<AddVideo> addVideo { get; set; }
        }
        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromBody] AddVideo rq)
        {
            rq.video.Id = Guid.NewGuid();
            var mapvideo = _mapper.Map<Video>(rq.video);
            await _context.videos.AddAsync(mapvideo);
            rq.test.Id = Guid.NewGuid();
            rq.test.VideoId = mapvideo.Id;
            rq.test.TotalQuestion = rq.questions.Count();
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
            rq.id = Guid.NewGuid();
            var map = _mapper.Map<Advertisement>(rq);
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
            foreach (var item in rq.Questions)
            {
                var checkitem = await _context.questions.FirstOrDefaultAsync(i => i.Id == item.QuestionId);
                if (item.Answer == checkitem.Answer)
                {
                    check++;
                }
            }
            rq.id = Guid.NewGuid();
            rq.Mark = check / (rq.Questions.Count());
            rq.Date = DateTime.Now;
            var map = _mapper.Map<HistoryTest>(rq);
            await _context.historyTests.AddAsync(map);
            await _context.SaveChangesAsync();
            return Ok(map);

        }
        public class RankDTO
        {
            public Guid UserId { get; set; }
            public float Mark { get; set; }
        }
        [HttpGet("Ranking")]
        public async Task<IActionResult> Ranking([FromQuery] Guid id)
        {
            var checkvideo = await _context.tests.FirstOrDefaultAsync(i => i.VideoId == id);
            var join = from s in _context.historyTests.Where(a => a.id == checkvideo.HistoryTest)
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
            }).OrderByDescending(a => a.Mark).ToList();
            return Ok(value);
        }
        [HttpGet("ViewVideoById")]
        public async Task<IActionResult> ViewVideo([FromQuery] Guid id)
        {
            return Ok(await _context.videos.FirstOrDefaultAsync(i => i.Id == id));
        }
        public class ViewTestDTOByCourseId
        {
            public Guid TestId { get; set; }
            public Guid VideoId { get; set; }
            public string NameVideo { get; set; }
            public string LinkVideo { get; set; }
            public string Description { get; set; }
            public string NameTest { get; set; }
            public string Content { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
        }
        public class ViewListTestDTOByCourseId
        {
            public Guid TestId { get; set; }

            public Guid   VideoId   { get; set; }
            public string NameVideo { get; set; }
            public string LinkVideo { get; set; }
            public string Description { get; set; }
            public string NameTest { get; set; }
            public List<AnswerTestDTOByCourseId> Answers { get; set; }

        }
        public class AnswerTestDTOByCourseId
        {
            public string Content { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
        }
        [HttpGet("ViewVideoByCourseId")]
        public async Task<IActionResult> ViewVideoByCourseId([FromQuery] Guid id)
        {
            var data = from s in _context.videos.Where(i => i.CourseId == id)
                       join sst in _context.tests on s.Id equals sst.VideoId
                       join st in _context.questions on sst.Id equals st.TestId
                       select new ViewTestDTOByCourseId
                       {
                           TestId=sst.Id,
                           VideoId     =s.Id            ,
                           NameVideo   =s.Name          ,
                           LinkVideo   =s.LinkVideo     ,
                           Description =s.Description   ,
                           NameTest = sst.Name,
                           Content = st.Content,
                           Answer1 = st.Answer1,
                           Answer2 = st.Answer2,
                           Answer3 = st.Answer3,
                           Answer4 = st.Answer4,

                       };
            var value = data.GroupBy(i => new { i.NameTest, i.VideoId,i.TestId, i.NameVideo, i.LinkVideo, i.Description }).Select(g => new ViewListTestDTOByCourseId
            {
                NameTest = g.Key.NameTest,
                TestId=g.Key.TestId,
                VideoId = g.Key.VideoId,
                NameVideo = g.Key.NameVideo,
                LinkVideo = g.Key.LinkVideo,
                Description = g.Key.Description,
                Answers = g.Select(a => new AnswerTestDTOByCourseId
                {

                    Content = a.Content,
                    Answer1 = a.Answer1,
                    Answer2 = a.Answer2,
                    Answer3 = a.Answer3,
                    Answer4 = a.Answer4

                }).ToList()
            }).ToList() ;
            return Ok(value);
        }
        [HttpGet("GetAllCourseByUserId")]
        public async Task<IActionResult> GetAllCourseByUserId([FromQuery] Guid userId)
        {
            return Ok(_context.courses.Where(i => i.UserId == userId).ToList());
        }
        [HttpGet("GetAllCourse")]
        public async Task<IActionResult> GetAllCourse()
        {
            return Ok(await _context.courses.ToListAsync());
        }
        public class ViewTestDTO
        {
            public string NameTest { get; set; }
            public string Content { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
        }
        public class ViewListTestDTO
        {
            public string NameTest { get; set; }
            public List<AnswerTestDTO> Answers { get; set; }

        }
        public class AnswerTestDTO
        {
            public string Content { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
        }
        [HttpGet("ViewTest")]
        public async Task<IActionResult> ViewTest()
        {
            var data = from s in _context.tests
                       join st in _context.questions on s.Id equals st.TestId
                       select new ViewTestDTO
                       {
                           NameTest = s.Name,
                           Content = st.Content,
                           Answer1 = st.Answer1,
                           Answer2 = st.Answer2,
                           Answer3 = st.Answer3,
                           Answer4 = st.Answer4,

                       };
            var value = data.GroupBy(i => i.NameTest).Select(g => new ViewListTestDTO
            {
                NameTest = g.Key,
                Answers = g.Select(a => new AnswerTestDTO
                {


                    Content = a.Content,
                    Answer1 = a.Answer1,
                    Answer2 = a.Answer2,
                    Answer3 = a.Answer3,
                    Answer4 = a.Answer4

                }).ToList()
            }).ToList();
            return Ok(value);
        }
        [HttpGet("ViewDetailTest")]
        public async Task<IActionResult> ViewDetailTest([FromQuery] Guid id)
        {
            var data = from s in _context.tests.Where(i => i.Id == id)
                       join st in _context.questions on s.Id equals st.TestId
                       select new ViewTestDTO
                       {
                           NameTest = s.Name,
                           Content = st.Content,
                           Answer1 = st.Answer1,
                           Answer2 = st.Answer2,
                           Answer3 = st.Answer3,
                           Answer4 = st.Answer4,

                       };
            var value = data.GroupBy(i => i.NameTest).Select(g => new ViewListTestDTO
            {
                NameTest = g.Key,
                Answers = g.Select(a => new AnswerTestDTO
                {


                    Content = a.Content,
                    Answer1 = a.Answer1,
                    Answer2 = a.Answer2,
                    Answer3 = a.Answer3,
                    Answer4 = a.Answer4

                }).ToList()
            });
            return Ok(value);
        }
        [HttpGet("ViewSupplier")]
        public async Task<IActionResult> ViewSupplier()
        {
            return Ok(await _context.suppliers.ToListAsync());
        }

    }
}
