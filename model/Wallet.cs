using System.ComponentModel.DataAnnotations;

namespace payment.model;

public class Wallet
{
    [Key] public string? UniqueWalletId { get; set; }
    [Required] public string? UserIdentifier { get; set; }
    [Required] public string? CountryCode { get; set; }
    public bool? IsDeleted { get; set; } = false;
    public bool? IsBusinessWallet { get; set; } = false;
    public string? LocationIdentifier { get; set; } = null;
    public string? CompanyCode { get; set; } = "HLA";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public double? Debit { get; set; } = 0.0;
}