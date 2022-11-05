using System.ComponentModel.DataAnnotations;

namespace payment.model;

public class Activity
{
    [Key] public int? Id { get; set; }
    public String? UserIdentifier { get; set; }
    public String? Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public String? CompanyCode { get; set; }
}