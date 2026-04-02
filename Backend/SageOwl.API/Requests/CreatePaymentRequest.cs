namespace SageOwl.API.Requests;

public class CreatePaymentRequest
{
    public Guid SubscriberId { get; set; }
    public int PlanId { get; set; }
    public decimal Amount { get; init; }
    public string Description { get; init; }
    public string SucessUrl { get; init; }
}
