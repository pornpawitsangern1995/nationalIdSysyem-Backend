namespace CitizenAPI.Models;

public class Citizen
{
    public int Id { get; set; }
    public string NationalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName  { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? SubDistrict { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public bool IsActive { get; set; } = true;
}