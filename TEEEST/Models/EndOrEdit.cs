// Models/EndOrEdit.cs
using System;
using System.Text.Json.Serialization;

public class EndOrEdit
{
    [JsonIgnore] // Ensures ID is not included in JSON responses
    public int Id { get; set; }

    public bool IsEndDay { get; set; }
    public string EditedObject { get; set; } = string.Empty;

    public decimal CardBefore { get; set; }
    public decimal CardAfter { get; set; }
    public decimal CashBefore { get; set; }
    public decimal CashAfter { get; set; }

    public DateTimeOffset OperationDate { get; set; }
    public string Username { get; set; } = string.Empty;

    public decimal AmountDifference { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; set; }
    public string Notes { get; set; } = string.Empty;
}
