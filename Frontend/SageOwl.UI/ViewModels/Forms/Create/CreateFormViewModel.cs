namespace SageOwl.UI.ViewModels.Forms.Create;

public class CreateFormViewModel
{
    public List<string> TeamNames { get; set; } = [];
    public CreateFormRequest NewForm { get; set; } = null!;
}
