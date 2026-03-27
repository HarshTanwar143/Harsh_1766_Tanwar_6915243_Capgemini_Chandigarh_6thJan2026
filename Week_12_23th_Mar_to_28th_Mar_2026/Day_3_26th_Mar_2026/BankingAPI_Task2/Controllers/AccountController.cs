using AutoMapper;
using BankingAPI_Task2.Data;
using BankingAPI_Task2.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingAPI_Task2.Controllers
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

        /// <summary>
        /// Task 2 Endpoint: GET /api/account/masked
        /// 
        /// - Requires JWT authentication
        /// - Returns AccountHolderName + MaskedAccountNumber
        /// - Masking is done by AutoMapper (MappingProfile), NOT here
        /// - Controller stays clean — zero masking logic here
        /// </summary>
        [HttpGet("masked")]
        [Authorize]
        public IActionResult GetMasked()
        {
            // Get logged-in user's ID from JWT claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);

            if (account == null)
                return NotFound("No account found for this user.");

            // AutoMapper handles masking internally — controller is clean
            var dto = _mapper.Map<AccountDTO>(account);

            return Ok(dto);
        }
    }
}
