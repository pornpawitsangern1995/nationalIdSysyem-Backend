namespace CitizenAPI.Services;

using CitizenAPI.Data;
using CitizenAPI.Models;
using CitizenAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

public interface ICitizenService
{
    Task<List<Citizen>> GetAllAsync();
    Task<Citizen?> GetByIdAsync(int id);
    Task<Citizen?> GetByNationalIdAsync(string nationalId);
    Task<Citizen> CreateAsync(CitizenDto dto);
    Task<Citizen?> UpdateAsync(int id, CitizenDto dto);
    Task<bool> DeleteAsync(int id);
}

public class CitizenService : ICitizenService
    {
        private readonly AppDbContext _db;
        public CitizenService(AppDbContext db) => _db = db;

        public async Task<List<Citizen>> GetAllAsync() =>
            await _db.Citizens
                     .Where(c => c.IsActive)
                     .OrderByDescending(c => c.UpdatedAt)
                     .ToListAsync();

        public async Task<Citizen?> GetByIdAsync(int id) =>
            await _db.Citizens.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

        public async Task<Citizen?> GetByNationalIdAsync(string nationalId) =>
            await _db.Citizens.FirstOrDefaultAsync(c => c.NationalId == nationalId && c.IsActive);

        public async Task<Citizen> CreateAsync(CitizenDto dto)
        {
            var citizen = new Citizen
            {
                NationalId = dto.NationalId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Address = dto.Address,
                Province = dto.Province,
                District = dto.District,
                SubDistrict = dto.SubDistrict,
                PostalCode = dto.PostalCode,
                PhoneNumber = dto.PhoneNumber,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _db.Citizens.Add(citizen);
            await _db.SaveChangesAsync();
            return citizen;
        }

        public async Task<Citizen?> UpdateAsync(int id, CitizenDto dto)
        {
            var citizen = await _db.Citizens.FindAsync(id);
            if (citizen == null || !citizen.IsActive) return null;

            citizen.NationalId  = dto.NationalId;
            citizen.FirstName = dto.FirstName;
            citizen.LastName = dto.LastName;
            citizen.BirthDate = dto.BirthDate;
            citizen.Address = dto.Address;
            citizen.Province = dto.Province;
            citizen.District = dto.District;
            citizen.SubDistrict = dto.SubDistrict;
            citizen.PostalCode = dto.PostalCode;
            citizen.PhoneNumber = dto.PhoneNumber;
            citizen.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();
            return citizen;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var citizen = await _db.Citizens.FindAsync(id);
            if (citizen == null) return false;
            citizen.IsActive  = false;
            citizen.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return true;
        }
    }

