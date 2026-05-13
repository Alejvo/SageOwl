namespace SageOwl.UI.ViewModels.Forms.Create;

public class CreateFormViewModel
{
    public Dictionary<Guid, string> TeamNames { get; set; } = [];
    public CreateFormRequest NewForm { get; set; } = null!;
}
