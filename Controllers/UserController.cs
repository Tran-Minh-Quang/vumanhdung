using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team12EUP.Data;
using Team12EUP.DTO;
using Team12EUP.Entity;

namespace Team12EUP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserController(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDTO rq)
        {
            var checkUsser = await _context.accounts.FirstOrDefaultAsync(i => i.UserName == rq.UserName);
            if (checkUsser != null) throw new Exception("Account exist");
            rq.Id = Guid.NewGuid();
            var mapacc = _mapper.Map<Account>(rq);
            await _context.accounts.AddAsync(mapacc);
            var mapUser = new User();
            mapUser.Id= mapacc.Id;
            mapUser.Role= rq.Role;
            mapUser.FullName = mapacc.UserName;
            await _context.users.AddAsync(mapUser);
            await _context.SaveChangesAsync();
            return Ok(mapacc.Id);

        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] string UserName , string Password)
        {
            var check = await _context.accounts.FirstOrDefaultAsync(i=>i.UserName==UserName&& i.Password==Password);
            if (check == null) return BadRequest(false);
            else return Ok(true);
        }

    }
}
