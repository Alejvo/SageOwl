namespace SageOwl.UI.ViewModels;

public class PlanViewModel
{
    public required string Title { get; set; }
    public decimal? YearlyCost { get; set; }
    public int NumberAccounts { get; set; } 
    public int NumberForms { get; set; } 
    public int NumberTeams { get; set; } 
}
