using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Team12EUP.Data;
using Team12EUP.Entity;

namespace Team12EUP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public VoucherController(DataContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public class SupplierDTO
        {
            [JsonIgnore]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Avatar { get; set; }
        }
        public class VoucherDTO
        {
            [JsonIgnore]
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime DateStart { get; set; }
            public DateTime DateEnd { get; set; }
            public string DetailContent { get; set; }
            public Guid Supplier { get; set; }
            public string QrCode { get; set; }

        }
        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier([FromBody] SupplierDTO rq)
        {
            rq.Id=Guid.NewGuid();
            var map = _mapper.Map<Supplier>(rq);
            await _context.suppliers.AddAsync(map);
            return Ok(map);
        }
        [HttpPost("AddVoucher")]
        public async Task<IActionResult> AddVoucher([FromBody] VoucherDTO rq)
        {
            rq.Id = Guid.NewGuid();
            var map = _mapper.Map<Voucher>(rq);
            await _context.vouchers.AddAsync(map);
            return Ok(map);
        }
        [HttpGet("ViewAllVoucher")]
        public async Task<IActionResult> ViewAllVoucher()
        {
            return Ok(await _context.vouchers.ToListAsync());
        }
        [HttpGet("ViewAllSupplier")]
        public async Task<IActionResult> ViewAllSupplier()
        {
            return Ok(await _context.suppliers.ToListAsync());
        }

    }
}
