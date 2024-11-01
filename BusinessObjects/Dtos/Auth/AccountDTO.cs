using System.Text.Json.Serialization;

namespace BusinessObjects.Dtos.Auth;

public class AccountResponseBasic {
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
    public string? Role { get; set; }

    public byte? Status { get; set; }

    public string? Email { get; set; }

    public double? Wallet { get; set; }
}