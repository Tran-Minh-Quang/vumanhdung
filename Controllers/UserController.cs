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
        public UserController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("Admin/RegisterFromAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AccountDTO rq)
        {
            var checkUsser = await _context.accounts.FirstOrDefaultAsync(i => i.UserName == rq.UserName);
            if (checkUsser != null) throw new Exception("Account exist");
            rq.Id = Guid.NewGuid();
            var mapacc = _mapper.Map<Account>(rq);
            await _context.accounts.AddAsync(mapacc);
            var mapUser = new User();
            mapUser.Id = mapacc.Id;
            mapUser.Role = rq.Role;
            mapUser.FullName = mapacc.UserName;
            await _context.users.AddAsync(mapUser);
            await _context.SaveChangesAsync();
            return Ok(mapacc.Id);

        }
        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] string UserName, string Password)
        {
            var check = await _context.accounts.FirstOrDefaultAsync(i => i.UserName == UserName && i.Password == Password);
            if (check == null) return BadRequest(false);
            else return Ok(true);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User rq)
        {
            var check = await _context.users.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check == null) throw new Exception("Fail");
            else
            {
                check.FullName = rq.FullName != null ? rq.FullName : check.FullName;
                check.Dob = rq.Dob != null ? rq.Dob : check.Dob;
                check.Role = rq.Role != null ? rq.Role : check.Role;
                check.Email = rq.Email != null ? rq.Email : check.Email;
                check.Address = rq.Address != null ? rq.Address : check.Address;
                check.PhoneNumber = rq.PhoneNumber != null ? rq.PhoneNumber : check.PhoneNumber;
                check.Gender = rq.Gender != null ? rq.Gender : check.Gender;
                await _context.SaveChangesAsync();
                return Ok(check.Id);
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDto rq)
        {
            var checkUsser = await _context.accounts.FirstOrDefaultAsync(i => i.UserName == rq.UserName);
            if (checkUsser != null) throw new Exception("Account exist");
            rq.Id = Guid.NewGuid();
            var mapacc = _mapper.Map<Account>(rq);
            await _context.accounts.AddAsync(mapacc);
            var mapUser = new User();
            mapUser.Id = mapacc.Id;
            mapUser.Role = 1;
            mapUser.FullName = mapacc.UserName;
            await _context.users.AddAsync(mapUser);
            await _context.SaveChangesAsync();
            return Ok(mapacc.Id);

        }
        [HttpPut("ChangePass")]
        public async Task<IActionResult> ChangePass([FromBody] string UserName , string Password , string NewPassword)
        {
            var check = await _context.accounts.FirstOrDefaultAsync(i => i.UserName == UserName && i.Password == Password);
            if (check == null) return BadRequest(false);
            else
            {
                check.Password = NewPassword;
                await _context.SaveChangesAsync();
                return Ok(check.Id);
            } 
                
        }    

    }
}
