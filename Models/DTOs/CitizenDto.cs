namespace CitizenAPI.Models.DTOs;

public class CitizenDto
{
    public string NationalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? SubDistrict { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? CreatedBy { get; set; }
}