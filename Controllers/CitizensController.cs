
    // ============================================================
    // FILE: Controllers/CitizensController.cs
    // ============================================================
    namespace CitizenAPI.Controllers;

    using CitizenAPI.Models.DTOs;
    using CitizenAPI.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CitizensController : ControllerBase
    {
        private readonly ICitizenService _svc;
        public CitizensController(ICitizenService svc) => _svc = svc;

        // GET /api/citizens
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _svc.GetAllAsync());

        // GET /api/citizens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _svc.GetByIdAsync(id);
            return c is null ? NotFound() : Ok(c);
        }

        // GET /api/citizens/national/1234567890123
        [HttpGet("national/{nationalId}")]
        public async Task<IActionResult> GetByNationalId(string nationalId)
        {
            var c = await _svc.GetByNationalIdAsync(nationalId);
            return c is null ? NotFound() : Ok(c);
        }

        // POST /api/citizens
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CitizenDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var exists = await _svc.GetByNationalIdAsync(dto.NationalId);
            if (exists != null) return Conflict(new { message = "เลขบัตรประชาชนนี้มีอยู่ในระบบแล้ว" });
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/citizens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CitizenDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _svc.UpdateAsync(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }

        // DELETE /api/citizens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _svc.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
