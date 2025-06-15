namespace SageOwl.UI.Models;

public record Plan(
    string Title,
    List<string> Characteristics,
    double? YearlyPrice,
    double? MonthlyPrice
    );

