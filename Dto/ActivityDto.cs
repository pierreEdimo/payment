namespace payment.Dto;

public class ActivityDto
{
    public int Id { get; set; }
    public String? UserIdentifier { get; set; } = null;
    public String? Body { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public String? CompanyCode { get; set; } = null;
}