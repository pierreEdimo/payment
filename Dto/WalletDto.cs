namespace payment.Dto;

public class WalletDto
{
    public string? UniqueWalletId { get; set; }
    public string? UserIdentifier { get; set; }
    public string? CountryCode { get; set; }
    public bool? IsDeleted { get; set; }
    public bool? IsBusinessWallet { get; set; }
    public string? LocationIdentifier { get; set; }
    public string? CompanyCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public double? Debit { get; set; }
}