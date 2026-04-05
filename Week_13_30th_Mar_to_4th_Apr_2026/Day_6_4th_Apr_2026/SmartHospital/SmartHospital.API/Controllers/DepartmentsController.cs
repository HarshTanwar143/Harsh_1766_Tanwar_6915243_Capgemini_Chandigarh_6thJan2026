using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentRepository _deptRepo;
    private readonly IMapper _mapper;

    public DepartmentsController(IDepartmentRepository deptRepo, IMapper mapper)
    {
        _deptRepo = deptRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var depts = await _deptRepo.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(depts));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dept = await _deptRepo.GetByIdAsync(id);
        if (dept == null) return NotFound();
        return Ok(_mapper.Map<DepartmentDto>(dept));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateDepartmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var dept = _mapper.Map<Department>(dto);
        await _deptRepo.AddAsync(dept);
        return Ok(new { message = "Department created.", dept.DepartmentId });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, CreateDepartmentDto dto)
    {
        var dept = await _deptRepo.GetByIdAsync(id);
        if (dept == null) return NotFound();
        dept.DepartmentName = dto.DepartmentName;
        dept.Description = dto.Description;
        await _deptRepo.UpdateAsync(dept);
        return Ok(new { message = "Updated." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deptRepo.DeleteAsync(id);
        return Ok(new { message = "Deleted." });
    }
}
