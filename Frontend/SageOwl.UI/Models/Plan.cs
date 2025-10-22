namespace SageOwl.UI.Models;

public class Plan
{
    public required string Title { get; set; }
    public decimal? YearlyCost { get; set; }
    public int NumberAccounts { get; set; }
    public int NumberForms { get; set; }
    public int NumberTeams { get; set; } 
};

