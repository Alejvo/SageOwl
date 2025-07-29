namespace SageOwl.UI.ViewModel;

public class PlanViewModel
{
    public required string Title { get; set; }
    public required List<string> Characteristics { get; set; }
    public decimal? MonthlyCost { get; set; }
    public decimal? YearlyCost { get; set; }
}
