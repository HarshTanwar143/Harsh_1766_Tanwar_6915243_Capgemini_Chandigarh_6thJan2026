using AutoMapper;
using BankingAPI.Data;
using BankingAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("details")]
        [Authorize]
        public IActionResult GetDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);

            if (account == null)
                return NotFound("Account not found.");

            var dto = _mapper.Map<AccountDTO>(account);

            return Ok(dto);
        }
    }
}
