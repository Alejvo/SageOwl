namespace SageOwl.UI.ViewModels.Forms;

public class QuestionViewModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description {  get; set; }
    public QuestionType Type { get; set; }
    public List<OptionViewModel> Options { get; set; } 
}
