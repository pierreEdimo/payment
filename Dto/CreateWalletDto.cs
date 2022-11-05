namespace payment.Dto;

public class CreateWalletDto
{
    public string? UserIdentifier {get; set;}
    public string? CountryCode{get; set; }
    public string? LocationIdentifier{get; set;}
    public string? CompanyCode{get; set;}
}