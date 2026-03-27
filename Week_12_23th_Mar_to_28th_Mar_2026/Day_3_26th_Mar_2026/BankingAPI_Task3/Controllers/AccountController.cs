using AutoMapper;
using BankingAPI_Task3.Data;
using BankingAPI_Task3.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankingAPI_Task3.Controllers
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
        /// Task 3 Core Endpoint: GET /api/account/details
        ///
        /// Same URL for both roles — but returns DIFFERENT data:
        ///   Admin  → AdminAccountDTO  (full: AccountNumber, Balance, Email)
        ///   User   → UserAccountDTO   (limited: MaskedAccountNumber only)
        ///
        /// Unauthorized users → 401
        /// </summary>
        [HttpGet("details")]
        [Authorize]  // any authenticated user can hit this
        public IActionResult GetDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role   = User.FindFirstValue(ClaimTypes.Role);

            var account = _context.Accounts.FirstOrDefault(a => a.UserId == userId);

            if (account == null)
                return NotFound("No account found for this user.");

            // Return DIFFERENT DTO based on role from JWT
            if (role == "Admin")
            {
                var adminDto = _mapper.Map<AdminAccountDTO>(account);
                return Ok(adminDto);
            }
            else
            {
                var userDto = _mapper.Map<UserAccountDTO>(account);
                return Ok(userDto);
            }
        }

        /// <summary>
        /// Admin-only endpoint — returns ALL accounts in the system.
        /// [Authorize(Roles = "Admin")] blocks non-admins with 403 Forbidden.
        /// Regular users → 403 Forbidden (not just 401)
        /// </summary>
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAccounts()
        {
            var accounts = _context.Accounts.ToList();
            var dtos = _mapper.Map<List<AdminAccountDTO>>(accounts);
            return Ok(dtos);
        }
    }
}
